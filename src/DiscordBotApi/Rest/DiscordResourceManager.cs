// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordResourceManager.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Concurrent;
using System.Text.RegularExpressions;

using DiscordBotApi.Models.Rest;

using Serilog;

namespace DiscordBotApi.Rest;

internal class DiscordResourceManager : IDiscordResourceManager, IDisposable
{
	private const string ApplicationsName = "applications";
	private const string ChannelsName = "channels";
	private const string GuildsName = "guilds";
	private const string InteractionsName = "interactions";
	private const string UnknownBucket = nameof(UnknownBucket);
	private const string UnlimitedBucket = nameof(UnlimitedBucket);
	private const string UsersName = "users";
	private const string WebhooksName = "webhooks";

	private readonly Regex _encodedEmojiRegex = new(
		pattern: @"(?<=reactions\/)([^%\/]*%[^%\/]*)+(?=\/|\?|$)",
		options: RegexOptions.Compiled);

	private readonly Regex _endpointNumberRegex = new(pattern: @"(?<=\/)\d+(?=\/|\?|$)", options: RegexOptions.Compiled);
	private readonly ILogger? _logger;

	private readonly BlockingCollection<DiscordResource> _reservationQueue =
		new(collection: new ConcurrentQueue<DiscordResource>());

	private readonly ConcurrentDictionary<DiscordResourceId, DiscordResource> _resources = new();
	private readonly Dictionary<string, DiscordRateLimit> _sharedLimits = new();
	private readonly Regex _webhookTokenRegex = new(pattern: @"\/[^\/]+", options: RegexOptions.Compiled);

	private CancellationTokenSource? _cancellationTokenSource;

	public DiscordResourceManager(ILogger? logger)
	{
		_logger = logger?.ForContext<DiscordResourceManager>();
	}

	public void Dispose()
	{
		_cancellationTokenSource?.Dispose();
		_reservationQueue.Dispose();
	}

	public async Task<IDisposable> GetReservationAsync(
		DiscordResourceId resourceId,
		long requestIndex,
		CancellationToken cancellationToken
	)
	{
		IDisposable? reservation = null;
		var resource = _resources.GetOrAdd(
			key: resourceId,
			valueFactory: _ =>
			{
				// Unknown rate limits (first resource access)
				var resource = new DiscordResource(id: resourceId, rateLimit: new DiscordRateLimit(Bucket: UnknownBucket));
				reservation = CreateReservationWithUpdate(resource: resource);
				return resource;
			});
		if (reservation != null)
		{
			return reservation;
		}

		var reservationRequest = new DiscordReservationRequest(
			ReservationReady: new TaskCompletionSource<IDisposable>(),
			CancellationToken: cancellationToken);
		lock (resource)
		{
			resource.ReservationRequests.Add(key: requestIndex, value: reservationRequest);
		}

		_reservationQueue.Add(item: resource, cancellationToken: cancellationToken);

		return await reservationRequest.ReservationReady.Task.WaitAsync(cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	public DiscordResourceId GetResourceId(string httpMethod, string endpoint)
	{
		var slashIndex = endpoint.IndexOf(value: '/');
		if (slashIndex == -1)
		{
			return new DiscordResourceId(HttpMethod: httpMethod, Path: endpoint);
		}

		var topLevelResourceName = endpoint[..slashIndex];
		switch (topLevelResourceName)
		{
			case ChannelsName:
				return GetResourceIdFromChannelsEndpoint(httpMethod: httpMethod, endpoint: endpoint);
			case GuildsName:
				return GetResourceIdFromGuildsEndpoint(httpMethod: httpMethod, endpoint: endpoint);
			case InteractionsName:
				return new DiscordResourceId(HttpMethod: httpMethod, Path: "interactions///callback");
			case WebhooksName:
				return GetResourceIdFromWebhooksEndpoint(httpMethod: httpMethod, endpoint: endpoint);
			case ApplicationsName:
			case UsersName:
				return GetResourceIdFromUnsupportedEndpoint(httpMethod: httpMethod, endpoint: endpoint);
			default:
				throw new NotSupportedException(message: $"Top-level resource '{topLevelResourceName}' is not supported");
		}
	}

	public void Start()
	{
		if (_cancellationTokenSource != null)
		{
			throw new InvalidOperationException(message: "Already started");
		}

		_cancellationTokenSource = new CancellationTokenSource();
		new TaskFactory().StartNew(
			action: () => ReservationProcessing(cancellationToken: _cancellationTokenSource.Token),
			creationOptions: TaskCreationOptions.LongRunning);
	}

	public void Stop()
	{
		if (_cancellationTokenSource == null)
		{
			throw new InvalidOperationException(message: "Already stopped");
		}

		_cancellationTokenSource.Cancel();
		_cancellationTokenSource = null;
	}

	public void UpdateResource(
		DiscordResourceId resourceId,
		DiscordBucketResponse? bucketResponse,
		DiscordRateLimitResponse? rateLimitResponse
	)
	{
		if (!_resources.TryGetValue(key: resourceId, value: out var resource))
		{
			throw new InvalidOperationException(message: "Resource does not exist.");
		}

		lock (resource)
		{
			if (bucketResponse == null)
			{
				SetUnlimitedBucket(resource: resource);
			}
			else
			{
				UpdateResourceBucket(resource: resource, bucketResponse: bucketResponse);
				UpdateRateLimit(rateLimit: resource.RateLimit, bucketResponse: bucketResponse);
			}

			resource.RateLimit.Retry = rateLimitResponse != null
				? DateTime.UtcNow.Add(value: rateLimitResponse.RetryAfter)
				: null;
		}
	}

	private static string RemoveQueryFromEndpoint(string endpoint)
	{
		var querySeparatorIndex = endpoint.IndexOf(value: '?');
		return querySeparatorIndex != -1
			? endpoint[..querySeparatorIndex]
			: endpoint;
	}

	private ResourceReservation CreateReservationWithUpdate(DiscordResource resource)
	{
		// The first thread making a reservation for a resource should discover
		// the current "resetAfter" and "remaining" values (returned by Discord).
		// Other threads should wait for this to finish, as "remaining" might be 0.
		var updating = new TaskCompletionSource(creationOptions: TaskCreationOptions.RunContinuationsAsynchronously);

		void SignalAvailable() =>
			// This is run when reservation is disposing.
			updating.SetResult();

		resource.RateLimit.UpdateTask = updating.Task;

		return new ResourceReservation(disposeAction: SignalAvailable);
	}

	private void GetOrCreateSharedLimit(DiscordResource resource, string bucket)
	{
		if (_sharedLimits.TryGetValue(key: bucket, value: out var rateLimit))
		{
			_logger?.Debug(
				messageTemplate: "Sharing RateLimitBucket ({Id}) for {Resource}",
				propertyValue0: bucket,
				propertyValue1: resource.Id);
			resource.RateLimit = rateLimit;
		}
		else
		{
			_logger?.Debug(
				messageTemplate: "Creating RateLimitBucket ({Id}) for {Resource}",
				propertyValue0: bucket,
				propertyValue1: resource.Id);
			resource.RateLimit = new DiscordRateLimit(Bucket: bucket);
			_sharedLimits.Add(key: bucket, value: resource.RateLimit);
		}
	}

	private DiscordResourceId GetResourceIdFromChannelsEndpoint(string httpMethod, string endpoint)
	{
		var resourcePath = RemoveQueryFromEndpoint(endpoint: endpoint);

		var firstSlashIndex = resourcePath.IndexOf(value: '/');
		var secondSlashIndex = resourcePath.IndexOf(value: '/', startIndex: firstSlashIndex + 1);
		if (secondSlashIndex == -1)
		{
			return new DiscordResourceId(HttpMethod: httpMethod, Path: resourcePath);
		}

		resourcePath = _endpointNumberRegex.Replace(
			input: resourcePath,
			replacement: "",
			count: int.MaxValue,
			startat: secondSlashIndex);
		resourcePath = _encodedEmojiRegex.Replace(
			input: resourcePath,
			replacement: "",
			count: int.MaxValue,
			startat: secondSlashIndex);

		return new DiscordResourceId(HttpMethod: httpMethod, Path: resourcePath);
	}

	private DiscordResourceId GetResourceIdFromGuildsEndpoint(string httpMethod, string endpoint)
	{
		var resourcePath = RemoveQueryFromEndpoint(endpoint: endpoint);

		var firstSlashIndex = resourcePath.IndexOf(value: '/');
		var secondSlashIndex = resourcePath.IndexOf(value: '/', startIndex: firstSlashIndex + 1);
		if (secondSlashIndex == -1)
		{
			return new DiscordResourceId(HttpMethod: httpMethod, Path: resourcePath);
		}

		resourcePath = _endpointNumberRegex.Replace(
			input: resourcePath,
			replacement: "",
			count: int.MaxValue,
			startat: secondSlashIndex);

		return new DiscordResourceId(HttpMethod: httpMethod, Path: resourcePath);
	}

	private DiscordResourceId GetResourceIdFromUnsupportedEndpoint(string httpMethod, string endpoint)
	{
		var resourcePath = _endpointNumberRegex.Replace(input: endpoint, replacement: "");

		return new DiscordResourceId(HttpMethod: httpMethod, Path: resourcePath);
	}

	private DiscordResourceId GetResourceIdFromWebhooksEndpoint(string httpMethod, string endpoint)
	{
		var resourcePath = RemoveQueryFromEndpoint(endpoint: endpoint);

		var firstSlashIndex = resourcePath.IndexOf(value: '/');
		var secondSlashIndex = resourcePath.IndexOf(value: '/', startIndex: firstSlashIndex + 1);
		if (secondSlashIndex == -1)
		{
			return new DiscordResourceId(HttpMethod: httpMethod, Path: resourcePath);
		}

		var webhookTokenMatch = _webhookTokenRegex.Match(input: resourcePath, startat: secondSlashIndex);
		resourcePath = webhookTokenMatch.Success
			? _endpointNumberRegex.Replace(
				input: resourcePath,
				replacement: "",
				count: int.MaxValue,
				startat: webhookTokenMatch.Index + webhookTokenMatch.Length)
			: _endpointNumberRegex.Replace(
				input: resourcePath,
				replacement: "",
				count: int.MaxValue,
				startat: secondSlashIndex);

		return new DiscordResourceId(HttpMethod: httpMethod, Path: resourcePath);
	}

	private void ReservationProcessing(CancellationToken cancellationToken)
	{
		// System clock resolution is about 15ms, but for handling rate limits add some margin
		// https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.delay?view=net-6.0
		var systemClockResolution = TimeSpan.FromMilliseconds(value: 50);

		var delayedResources = new ConcurrentDictionary<DiscordResourceId, Task>();

		async Task DelayResourceAsync(DiscordResource resource, Func<Task> taskFactory)
		{
			var isAdded = false;
			var delayTask = delayedResources.GetOrAdd(
				key: resource.Id,
				valueFactory: _ =>
				{
					isAdded = true;
					return taskFactory();
				});

			if (isAdded)
			{
				await delayTask.ConfigureAwait(continueOnCapturedContext: false);
				_ = delayedResources.TryRemove(key: resource.Id, value: out _);
				_reservationQueue.Add(item: resource, cancellationToken: CancellationToken.None);
			}
		}

		while (!cancellationToken.IsCancellationRequested)
		{
			var resource = _reservationQueue.Take(cancellationToken: cancellationToken);

			if (delayedResources.ContainsKey(key: resource.Id))
			{
				// Resource is rate limited
				continue;
			}

			if (!resource.ReservationRequests.Any())
			{
				// Resource was added multiple times
				// All requests has already been processed
				continue;
			}

			lock (resource)
			{
				var utcNow = DateTime.UtcNow;
				if (resource.RateLimit.Retry != null &&
					resource.RateLimit.Retry > utcNow)
				{
					// Resource is rate limited due to shared/global limit
					var retryAfter = (resource.RateLimit.Retry.Value - utcNow).Add(ts: systemClockResolution);

					_logger?.Debug(
						messageTemplate: "Resource shared rate limit; waiting {Count:F3} seconds -- {Id}/{Resource}",
						propertyValue0: retryAfter.TotalSeconds,
						propertyValue1: resource.RateLimit.Bucket,
						propertyValue2: resource.Id);
					_ = DelayResourceAsync(
						resource: resource,
						taskFactory: () => Task.Delay(delay: retryAfter, cancellationToken: cancellationToken));
					continue;
				}

				if (resource.RateLimit.Bucket == UnlimitedBucket)
				{
					// Resource has no bucket limit
					while (resource.ReservationRequests.Any())
					{
						var (requestIndex, reservationRequest) = resource.ReservationRequests.First();
						resource.ReservationRequests.Remove(key: requestIndex);
						reservationRequest.ReservationReady.SetResult(result: new ResourceReservation());
					}

					continue;
				}

				if (!resource.RateLimit.UpdateTask.IsCompleted)
				{
					// Resource is blocked due to being updated
					_logger?.Debug(
						messageTemplate: "Resoure is updating; {Id}/{Resource}",
						propertyValue0: resource.RateLimit.Bucket,
						propertyValue1: resource.Id);
					_ = DelayResourceAsync(
						resource: resource,
						taskFactory: () => resource.RateLimit.UpdateTask.WaitAsync(cancellationToken: cancellationToken));
					continue;
				}

				if (resource.RateLimit.Reset <= utcNow)
				{
					// Current rate limits are outdated
					if (resource.ReservationRequests.Any())
					{
						var (requestIndex, reservationRequest) = resource.ReservationRequests.First();
						resource.ReservationRequests.Remove(key: requestIndex);
						reservationRequest.ReservationReady.SetResult(result: CreateReservationWithUpdate(resource: resource));
					}

					_ = DelayResourceAsync(
						resource: resource,
						taskFactory: () => resource.RateLimit.UpdateTask.WaitAsync(cancellationToken: cancellationToken));
					continue;
				}

				if (resource.RateLimit.Remaining > 0)
				{
					while (resource.RateLimit.Remaining > 0 &&
						resource.ReservationRequests.Any())
					{
						var (requestIndex, reservationRequest) = resource.ReservationRequests.First();
						resource.ReservationRequests.Remove(key: requestIndex);

						// Resource has remaining quota
						resource.RateLimit.Remaining--;
						reservationRequest.ReservationReady.SetResult(result: new ResourceReservation());
					}

					if (!resource.ReservationRequests.Any())
					{
						continue;
					}
				}

				// Resource has no quota left
				var resetAfter = (resource.RateLimit.Reset - utcNow).Add(ts: systemClockResolution);

				_logger?.Debug(
					messageTemplate: "Resource endpoint rate limit; waiting {Count:F3} seconds -- {Id}/{Resource}",
					propertyValue0: resetAfter.TotalSeconds,
					propertyValue1: resource.RateLimit.Bucket,
					propertyValue2: resource.Id);
				_ = DelayResourceAsync(
					resource: resource,
					taskFactory: () => Task.Delay(delay: resetAfter, cancellationToken: cancellationToken));
			}
		}
	}

	private void ResetSharedLimit(DiscordResource resource, string bucket)
	{
		_sharedLimits.Remove(key: resource.RateLimit.Bucket);
		GetOrCreateSharedLimit(resource: resource, bucket: bucket);
	}

	private void SetUnlimitedBucket(DiscordResource resource)
	{
		if (resource.RateLimit.Bucket == UnlimitedBucket)
		{
			return;
		}

		_logger?.Debug(
			messageTemplate: "Resource {Resource} has changed bucket from {OldBucket} to {NewBucket}",
			propertyValue0: resource.Id,
			propertyValue1: resource.RateLimit.Bucket,
			propertyValue2: UnlimitedBucket);
		resource.RateLimit = new DiscordRateLimit(Bucket: UnlimitedBucket);
	}

	private void UpdateRateLimit(DiscordRateLimit rateLimit, DiscordBucketResponse bucketResponse)
	{
		var reset = DateTime.UtcNow.Add(value: bucketResponse.ResetAfter);
		if (rateLimit.DiscordReset < bucketResponse.DiscordReset)
		{
			// New rate limit bucket period.
			rateLimit.DiscordReset = bucketResponse.DiscordReset;

			rateLimit.Reset = reset;
			rateLimit.Remaining = bucketResponse.Remaining;
		}
		else
		{
			// Same rate limit bucket period.
			// Since threads update in any order, pick the "worst" values.
			if (reset > rateLimit.Reset)
			{
				rateLimit.Reset = reset;
			}

			if (bucketResponse.Remaining < rateLimit.Remaining)
			{
				rateLimit.Remaining = bucketResponse.Remaining;
			}
		}
	}

	private void UpdateResourceBucket(DiscordResource resource, DiscordBucketResponse bucketResponse)
	{
		if (resource.RateLimit.Bucket == UnknownBucket)
		{
			_logger?.Debug(
				messageTemplate: "Resource {Resource} has changed bucket from {OldBucket} to {NewBucket}",
				propertyValue0: resource.Id,
				propertyValue1: UnknownBucket,
				propertyValue2: bucketResponse.Bucket);
			GetOrCreateSharedLimit(resource: resource, bucket: bucketResponse.Bucket);
		}
		else if (bucketResponse.Bucket != resource.RateLimit.Bucket)
		{
			_logger?.Debug(
				messageTemplate: "Resource {Resource} has changed bucket from {OldBucket} to {NewBucket}",
				propertyValue0: resource.Id,
				propertyValue1: resource.RateLimit.Bucket,
				propertyValue2: bucketResponse.Bucket);
			ResetSharedLimit(resource: resource, bucket: bucketResponse.Bucket);
		}
	}

	private class ResourceReservation : IDisposable
	{
		private readonly Action? _disposeAction;

		public ResourceReservation(Action? disposeAction = null)
		{
			_disposeAction = disposeAction;
		}

		public void Dispose() => _disposeAction?.Invoke();
	}
}