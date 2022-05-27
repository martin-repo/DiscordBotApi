// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordResourceManager.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Rest
{
    using System.Collections.Concurrent;
    using System.Text.RegularExpressions;

    using DiscordBotApi.Models.Rest;

    using Serilog;

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

        private readonly Regex _encodedEmojiRegex = new(@"(?<=reactions\/)([^%\/]*%[^%\/]*)+(?=\/|\?|$)", RegexOptions.Compiled);
        private readonly Regex _endpointNumberRegex = new(@"(?<=\/)\d+(?=\/|\?|$)", RegexOptions.Compiled);
        private readonly ILogger? _logger;
        private readonly BlockingCollection<DiscordResource> _reservationQueue = new(new ConcurrentQueue<DiscordResource>());
        private readonly ConcurrentDictionary<DiscordResourceId, DiscordResource> _resources = new();
        private readonly Dictionary<string, DiscordRateLimit> _sharedLimits = new();
        private readonly Regex _webhookTokenRegex = new(@"\/[^\/]+", RegexOptions.Compiled);

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

        public async Task<IDisposable> GetReservationAsync(DiscordResourceId resourceId, long requestIndex, CancellationToken cancellationToken)
        {
            IDisposable? reservation = null;
            var resource = _resources.GetOrAdd(
                resourceId,
                _ =>
                {
                    // Unknown rate limits (first resource access)
                    var resource = new DiscordResource(resourceId, new DiscordRateLimit(UnknownBucket));
                    reservation = CreateReservationWithUpdate(resource);
                    return resource;
                });
            if (reservation != null)
            {
                return reservation;
            }

            var reservationRequest = new DiscordReservationRequest(new TaskCompletionSource<IDisposable>(), cancellationToken);
            lock (resource)
            {
                resource.ReservationRequests.Add(requestIndex, reservationRequest);
            }

            _reservationQueue.Add(resource, cancellationToken);

            return await reservationRequest.ReservationReady.Task.WaitAsync(cancellationToken).ConfigureAwait(false);
        }

        public DiscordResourceId GetResourceId(string httpMethod, string endpoint)
        {
            var slashIndex = endpoint.IndexOf('/');
            if (slashIndex == -1)
            {
                return new(httpMethod, endpoint);
            }

            var topLevelResourceName = endpoint[..slashIndex];
            switch (topLevelResourceName)
            {
                case ChannelsName:
                    return GetResourceIdFromChannelsEndpoint(httpMethod, endpoint);
                case GuildsName:
                    return GetResourceIdFromGuildsEndpoint(httpMethod, endpoint);
                case InteractionsName:
                    return new(httpMethod, "interactions///callback");
                case WebhooksName:
                    return GetResourceIdFromWebhooksEndpoint(httpMethod, endpoint);
                case ApplicationsName:
                case UsersName:
                    return GetResourceIdFromUnsupportedEndpoint(httpMethod, endpoint);
                default:
                    throw new NotSupportedException($"Top-level resource '{topLevelResourceName}' is not supported");
            }
        }

        public void Start()
        {
            if (_cancellationTokenSource != null)
            {
                throw new InvalidOperationException("Already started");
            }

            _cancellationTokenSource = new CancellationTokenSource();
            new TaskFactory().StartNew(() => ReservationProcessing(_cancellationTokenSource.Token), TaskCreationOptions.LongRunning);
        }

        public void Stop()
        {
            if (_cancellationTokenSource == null)
            {
                throw new InvalidOperationException("Already stopped");
            }

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = null;
        }

        public void UpdateResource(DiscordResourceId resourceId, DiscordBucketResponse? bucketResponse, DiscordRateLimitResponse? rateLimitResponse)
        {
            if (!_resources.TryGetValue(resourceId, out var resource))
            {
                throw new InvalidOperationException("Resource does not exist.");
            }

            lock (resource)
            {
                if (bucketResponse == null)
                {
                    SetUnlimitedBucket(resource);
                }
                else
                {
                    UpdateResourceBucket(resource, bucketResponse);
                    UpdateRateLimit(resource.RateLimit, bucketResponse);
                }

                resource.RateLimit.Retry = rateLimitResponse != null ? DateTime.UtcNow.Add(rateLimitResponse.RetryAfter) : null;
            }
        }

        private static string RemoveQueryFromEndpoint(string endpoint)
        {
            var querySeparatorIndex = endpoint.IndexOf('?');
            return querySeparatorIndex != -1 ? endpoint[..querySeparatorIndex] : endpoint;
        }

        private ResourceReservation CreateReservationWithUpdate(DiscordResource resource)
        {
            // The first thread making a reservation for a resource should discover
            // the current "resetAfter" and "remaining" values (returned by Discord).
            // Other threads should wait for this to finish, as "remaining" might be 0.
            var updating = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

            void SignalAvailable()
            {
                // This is run when reservation is disposing.
                updating.SetResult();
            }

            resource.RateLimit.UpdateTask = updating.Task;

            return new ResourceReservation(SignalAvailable);
        }

        private void GetOrCreateSharedLimit(DiscordResource resource, string bucket)
        {
            if (_sharedLimits.TryGetValue(bucket, out var rateLimit))
            {
                _logger?.Debug("Sharing RateLimitBucket ({Id}) for {Resource}", bucket, resource.Id);
                resource.RateLimit = rateLimit;
            }
            else
            {
                _logger?.Debug("Creating RateLimitBucket ({Id}) for {Resource}", bucket, resource.Id);
                resource.RateLimit = new DiscordRateLimit(bucket);
                _sharedLimits.Add(bucket, resource.RateLimit);
            }
        }

        private DiscordResourceId GetResourceIdFromChannelsEndpoint(string httpMethod, string endpoint)
        {
            var resourcePath = RemoveQueryFromEndpoint(endpoint);

            var firstSlashIndex = resourcePath.IndexOf('/');
            var secondSlashIndex = resourcePath.IndexOf('/', firstSlashIndex + 1);
            if (secondSlashIndex == -1)
            {
                return new(httpMethod, resourcePath);
            }

            resourcePath = _endpointNumberRegex.Replace(resourcePath, "", int.MaxValue, secondSlashIndex);
            resourcePath = _encodedEmojiRegex.Replace(resourcePath, "", int.MaxValue, secondSlashIndex);

            return new(httpMethod, resourcePath);
        }

        private DiscordResourceId GetResourceIdFromGuildsEndpoint(string httpMethod, string endpoint)
        {
            var resourcePath = RemoveQueryFromEndpoint(endpoint);

            var firstSlashIndex = resourcePath.IndexOf('/');
            var secondSlashIndex = resourcePath.IndexOf('/', firstSlashIndex + 1);
            if (secondSlashIndex == -1)
            {
                return new(httpMethod, resourcePath);
            }

            resourcePath = _endpointNumberRegex.Replace(resourcePath, "", int.MaxValue, secondSlashIndex);

            return new(httpMethod, resourcePath);
        }

        private DiscordResourceId GetResourceIdFromUnsupportedEndpoint(string httpMethod, string endpoint)
        {
            var resourcePath = _endpointNumberRegex.Replace(endpoint, "");

            return new(httpMethod, resourcePath);
        }

        private DiscordResourceId GetResourceIdFromWebhooksEndpoint(string httpMethod, string endpoint)
        {
            var resourcePath = RemoveQueryFromEndpoint(endpoint);

            var firstSlashIndex = resourcePath.IndexOf('/');
            var secondSlashIndex = resourcePath.IndexOf('/', firstSlashIndex + 1);
            if (secondSlashIndex == -1)
            {
                return new(httpMethod, resourcePath);
            }

            var webhookTokenMatch = _webhookTokenRegex.Match(resourcePath, secondSlashIndex);
            resourcePath = webhookTokenMatch.Success
                               ? _endpointNumberRegex.Replace(resourcePath, "", int.MaxValue, webhookTokenMatch.Index + webhookTokenMatch.Length)
                               : _endpointNumberRegex.Replace(resourcePath, "", int.MaxValue, secondSlashIndex);

            return new(httpMethod, resourcePath);
        }

        private void ReservationProcessing(CancellationToken cancellationToken)
        {
            // System clock resolution is about 15ms, but for handling rate limits add some margin
            // https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.delay?view=net-6.0
            var systemClockResolution = TimeSpan.FromMilliseconds(50);

            var delayedResources = new ConcurrentDictionary<DiscordResourceId, Task>();

            async Task DelayResourceAsync(DiscordResource resource, Func<Task> taskFactory)
            {
                var isAdded = false;
                var delayTask = delayedResources.GetOrAdd(
                    resource.Id,
                    _ =>
                    {
                        isAdded = true;
                        return taskFactory();
                    });

                if (isAdded)
                {
                    await delayTask.ConfigureAwait(false);
                    _ = delayedResources.TryRemove(resource.Id, out _);
                    _reservationQueue.Add(resource, CancellationToken.None);
                }
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                var resource = _reservationQueue.Take(cancellationToken);

                if (delayedResources.ContainsKey(resource.Id))
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
                    if (resource.RateLimit.Retry != null && resource.RateLimit.Retry > utcNow)
                    {
                        // Resource is rate limited due to shared/global limit
                        var retryAfter = (resource.RateLimit.Retry.Value - utcNow).Add(systemClockResolution);

                        _logger?.Debug(
                            "Resource shared rate limit; waiting {Count:F3} seconds -- {Id}/{Resource}",
                            retryAfter.TotalSeconds,
                            resource.RateLimit.Bucket,
                            resource.Id);
                        _ = DelayResourceAsync(resource, () => Task.Delay(retryAfter, cancellationToken));
                        continue;
                    }

                    if (resource.RateLimit.Bucket == UnlimitedBucket)
                    {
                        // Resource has no bucket limit
                        while (resource.ReservationRequests.Any())
                        {
                            var (requestIndex, reservationRequest) = resource.ReservationRequests.First();
                            resource.ReservationRequests.Remove(requestIndex);
                            reservationRequest.ReservationReady.SetResult(new ResourceReservation());
                        }

                        continue;
                    }

                    if (!resource.RateLimit.UpdateTask.IsCompleted)
                    {
                        // Resource is blocked due to being updated
                        _logger?.Debug("Resoure is updating; {Id}/{Resource}", resource.RateLimit.Bucket, resource.Id);
                        _ = DelayResourceAsync(resource, () => resource.RateLimit.UpdateTask.WaitAsync(cancellationToken));
                        continue;
                    }

                    if (resource.RateLimit.Reset <= utcNow)
                    {
                        // Current rate limits are outdated
                        if (resource.ReservationRequests.Any())
                        {
                            var (requestIndex, reservationRequest) = resource.ReservationRequests.First();
                            resource.ReservationRequests.Remove(requestIndex);
                            reservationRequest.ReservationReady.SetResult(CreateReservationWithUpdate(resource));
                        }

                        _ = DelayResourceAsync(resource, () => resource.RateLimit.UpdateTask.WaitAsync(cancellationToken));
                        continue;
                    }

                    if (resource.RateLimit.Remaining > 0)
                    {
                        while (resource.RateLimit.Remaining > 0 && resource.ReservationRequests.Any())
                        {
                            var (requestIndex, reservationRequest) = resource.ReservationRequests.First();
                            resource.ReservationRequests.Remove(requestIndex);

                            // Resource has remaining quota
                            resource.RateLimit.Remaining--;
                            reservationRequest.ReservationReady.SetResult(new ResourceReservation());
                        }

                        if (!resource.ReservationRequests.Any())
                        {
                            continue;
                        }
                    }

                    // Resource has no quota left
                    var resetAfter = (resource.RateLimit.Reset - utcNow).Add(systemClockResolution);

                    _logger?.Debug(
                        "Resource endpoint rate limit; waiting {Count:F3} seconds -- {Id}/{Resource}",
                        resetAfter.TotalSeconds,
                        resource.RateLimit.Bucket,
                        resource.Id);
                    _ = DelayResourceAsync(resource, () => Task.Delay(resetAfter, cancellationToken));
                }
            }
        }

        private void ResetSharedLimit(DiscordResource resource, string bucket)
        {
            _sharedLimits.Remove(resource.RateLimit.Bucket);
            GetOrCreateSharedLimit(resource, bucket);
        }

        private void SetUnlimitedBucket(DiscordResource resource)
        {
            if (resource.RateLimit.Bucket == UnlimitedBucket)
            {
                return;
            }

            _logger?.Debug(
                "Resource {Resource} has changed bucket from {OldBucket} to {NewBucket}",
                resource.Id,
                resource.RateLimit.Bucket,
                UnlimitedBucket);
            resource.RateLimit = new DiscordRateLimit(UnlimitedBucket);
        }

        private void UpdateRateLimit(DiscordRateLimit rateLimit, DiscordBucketResponse bucketResponse)
        {
            var reset = DateTime.UtcNow.Add(bucketResponse.ResetAfter);
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
                    "Resource {Resource} has changed bucket from {OldBucket} to {NewBucket}",
                    resource.Id,
                    UnknownBucket,
                    bucketResponse.Bucket);
                GetOrCreateSharedLimit(resource, bucketResponse.Bucket);
            }
            else if (bucketResponse.Bucket != resource.RateLimit.Bucket)
            {
                _logger?.Debug(
                    "Resource {Resource} has changed bucket from {OldBucket} to {NewBucket}",
                    resource.Id,
                    resource.RateLimit.Bucket,
                    bucketResponse.Bucket);
                ResetSharedLimit(resource, bucketResponse.Bucket);
            }
        }

        private class ResourceReservation : IDisposable
        {
            private readonly Action? _disposeAction;

            public ResourceReservation(Action? disposeAction = null)
            {
                _disposeAction = disposeAction;
            }

            public void Dispose()
            {
                _disposeAction?.Invoke();
            }
        }
    }
}