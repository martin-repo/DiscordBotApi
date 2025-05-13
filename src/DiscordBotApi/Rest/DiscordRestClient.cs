// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRestClient.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Rest;
using DiscordBotApi.Models.Rest;

using Serilog;

namespace DiscordBotApi.Rest;

internal class DiscordRestClient : IDisposable
{
	private const string JsonContentType = "application/json";
	private const int MaxSimultaneousConnections = 100;
	private const string RateLimitBucket = "X-RateLimit-Bucket";
	private const string RateLimitLimit = "X-RateLimit-Limit";
	private const string RateLimitRemaining = "X-RateLimit-Remaining";
	private const string RateLimitReset = "X-RateLimit-Reset";
	private const string RateLimitResetAfter = "X-RateLimit-Reset-After";
	private const string RateLimitScope = "X-RateLimit-Scope";

	private readonly IDiscordGlobalManager _globalManager;
	private readonly HttpClient _httpClient;

	private readonly SemaphoreSlim _httpClientAccess = new(
		initialCount: MaxSimultaneousConnections,
		maxCount: MaxSimultaneousConnections
	);

	private readonly ILogger? _logger;
	private readonly IDiscordResourceManager _resourceManager;

	private long _requestIndex;

	public DiscordRestClient(
		ILogger? logger,
		HttpClient httpClient,
		IDiscordGlobalManager globalManager,
		IDiscordResourceManager resourceManager,
		string baseUrl,
		string botToken
	)
	{
		_logger = logger?.ForContext<DiscordRestClient>();

		_httpClient = httpClient;
		_httpClient.BaseAddress = new Uri(uriString: baseUrl);

		var version = Assembly.GetExecutingAssembly().GetName().Version;
		if (version == null)
		{
			throw new InvalidOperationException(message: "Failed to extract assembly version.");
		}

		_logger?.Debug(
			messageTemplate: "{Name} {Version}",
			propertyValue0: nameof(DiscordBotClient),
			propertyValue1: version
		);

		_httpClient.DefaultRequestHeaders.UserAgent.Add(
			item: new ProductInfoHeaderValue(
				productName: nameof(DiscordBotClient),
				productVersion: version.ToString(fieldCount: 3)
			)
		);
		_httpClient.DefaultRequestHeaders.Add(name: "Authorization", value: $"Bot {botToken}");

		_globalManager = globalManager;
		_resourceManager = resourceManager;
	}

	~DiscordRestClient()
	{
		Dispose(disposeManaged: false);
	}

	public event EventHandler<DiscordRateLimitExceeded>? RateLimitExceeded;

	public HttpContent CreateJsonContent(object value)
	{
		var json = JsonSerializer.Serialize(
			value: value,
			options: new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }
		);
		var jsonContent = new StringContent(content: json, encoding: Encoding.UTF8, mediaType: JsonContentType);
		return jsonContent;
	}

	public void Dispose()
	{
		Dispose(disposeManaged: true);
		GC.SuppressFinalize(obj: this);
	}

	public async Task SendRequestAsync(
		Func<HttpRequestMessage> requestFactoryFunc,
		HttpStatusCode expectedResponseCode,
		CancellationToken cancellationToken
	)
	{
		using var response = await SendAndWaitIfRateLimitExceededAsync(
				requestFactoryFunc: requestFactoryFunc,
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);
		if (response.StatusCode != expectedResponseCode)
		{
			var contentJson = await response
				.Content.ReadAsStringAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
			var errorResponse = ParseDiscordErrorResponse(json: contentJson);
			throw new DiscordRestException(statusCode: response.StatusCode, errorResponse: errorResponse);
		}
	}

	public async Task<T> SendRequestAsync<T>(
		Func<HttpRequestMessage> requestFactoryFunc,
		CancellationToken cancellationToken
	)
		where T : class
	{
		using var response = await SendAndWaitIfRateLimitExceededAsync(
				requestFactoryFunc: requestFactoryFunc,
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);
		var contentJson = await response
			.Content.ReadAsStringAsync(cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		if (!response.IsSuccessStatusCode)
		{
			var errorResponse = ParseDiscordErrorResponse(json: contentJson);
			throw new DiscordRestException(statusCode: response.StatusCode, errorResponse: errorResponse);
		}

		var content = JsonSerializer.Deserialize<T>(json: contentJson);
		if (content == null)
		{
			throw new SerializationException(message: "Failed to deserialize Json.");
		}

		return content;
	}

	public async Task<string> SendRequestAsync(
		Func<HttpRequestMessage> requestFactoryFunc,
		CancellationToken cancellationToken
	)
	{
		using var response = await SendAndWaitIfRateLimitExceededAsync(
				requestFactoryFunc: requestFactoryFunc,
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);
		var contentJson = await response
			.Content.ReadAsStringAsync(cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		if (response.IsSuccessStatusCode)
		{
			return contentJson;
		}

		var errorResponse = ParseDiscordErrorResponse(json: contentJson);
		throw new DiscordRestException(statusCode: response.StatusCode, errorResponse: errorResponse);
	}

	private static string? GetHeaderValue(HttpResponseHeaders headers, string key)
	{
		if (!headers.TryGetValues(name: key, values: out var values))
		{
			return null;
		}

		var valueArray = values.ToArray();
		return valueArray.Length == 1
			? valueArray[0]
			: throw new InvalidOperationException(message: $"Headers contains multiple values for {key}");
	}

	private static DiscordErrorResponse ParseDiscordErrorResponse(string json)
	{
		DiscordErrorResponseDto? errorResponseDto;
		try
		{
			errorResponseDto = JsonSerializer.Deserialize<DiscordErrorResponseDto>(json: json);
		}
		catch (JsonException)
		{
			return new DiscordErrorResponse
			{
				Code = DiscordJsonErrorCode.GeneralError,
				Message = "Invalid error response",
				JsonKey = json
			};
		}

		if (errorResponseDto == null)
		{
			return new DiscordErrorResponse
			{
				Code = DiscordJsonErrorCode.GeneralError,
				Message = "Unknown error",
				JsonKey = json
			};
		}

		var errorResponse = errorResponseDto.ToModel();
		return errorResponse;
	}

	private static async Task<DiscordRateLimitResponse> ParseRateLimitResponseAsync(HttpResponseMessage response)
	{
		var contentJson = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
		var responseDto = JsonSerializer.Deserialize<DiscordRateLimitResponseDto>(json: contentJson)!;
		return responseDto.ToModel();
	}

	private static DiscordRateLimitScope ParseRateLimitScope(HttpResponseMessage response)
	{
		var scopeString = GetHeaderValue(headers: response.Headers, key: RateLimitScope);
		switch (scopeString)
		{
			case "user": return DiscordRateLimitScope.User;
			case "global": return DiscordRateLimitScope.Global;
			case "shared": return DiscordRateLimitScope.Shared;
			default:
				throw new NotSupportedException(message: $"{scopeString} is not a valid {nameof(DiscordRateLimitScope)}");
		}
	}

	private void Dispose(bool disposeManaged)
	{
		if (disposeManaged)
		{
			_httpClient.Dispose();
		}
	}

	private void LogBucketResponse(
		DiscordResourceId resource,
		HttpResponseMessage response,
		DiscordBucketResponse? bucketResponse
	)
	{
		if (bucketResponse != null)
		{
			_logger?.Debug(
				messageTemplate: "{Code} {Bucket} {Remaining}/{Limit} {ResetAfter} - {Resource}",
				(int)response.StatusCode,
				bucketResponse.Bucket,
				bucketResponse.Remaining,
				bucketResponse.Limit,
				bucketResponse.ResetAfter.TotalSeconds + "s",
				resource
			);
		}
		else
		{
			_logger?.Debug(
				messageTemplate: "{Code} - {Resource}",
				propertyValue0: (int)response.StatusCode,
				propertyValue1: resource
			);
		}
	}

	private DiscordBucketResponse? ParseBucketResponse(HttpResponseMessage response)
	{
		var headers = response.Headers;

		var bucket = GetHeaderValue(headers: headers, key: RateLimitBucket);
		if (bucket == null)
		{
			return null;
		}

		var limitValue = GetHeaderValue(headers: headers, key: RateLimitLimit);
		var limit = int.Parse(s: limitValue!);

		var remainingValue = GetHeaderValue(headers: headers, key: RateLimitRemaining);
		var remaining = int.Parse(s: remainingValue!);

		var resetValue = GetHeaderValue(headers: headers, key: RateLimitReset);
		var resetSeconds = double.Parse(s: resetValue!, provider: CultureInfo.InvariantCulture);

		var resetAfterValue = GetHeaderValue(headers: headers, key: RateLimitResetAfter);
		var resetAfterSeconds = double.Parse(s: resetAfterValue!, provider: CultureInfo.InvariantCulture);
		var resetAfter = TimeSpan.FromSeconds(value: resetAfterSeconds);

		return new DiscordBucketResponse
		{
			Bucket = bucket,
			Limit = limit,
			Remaining = remaining,
			DiscordReset = resetSeconds,
			ResetAfter = resetAfter
		};
	}

	private async Task<HttpResponseMessage> SendAndWaitIfRateLimitExceededAsync(
		Func<HttpRequestMessage> requestFactoryFunc,
		CancellationToken cancellationToken
	)
	{
		using var requestCancellationTokenSource =
			CancellationTokenSource.CreateLinkedTokenSource(token: cancellationToken);
		var requestToken = requestCancellationTokenSource.Token;

		var requestIndex = Interlocked.Increment(location: ref _requestIndex);
		while (!requestToken.IsCancellationRequested)
		{
			var request = requestFactoryFunc();
			var resource = _resourceManager.GetResourceId(
				httpMethod: request.Method.ToString(),
				endpoint: request.RequestUri!.OriginalString
			);

			using var reservation = await _resourceManager
				.GetReservationAsync(resourceId: resource, requestIndex: requestIndex, cancellationToken: requestToken)
				.ConfigureAwait(continueOnCapturedContext: false);
			await _globalManager
				.GetReservationAsync(resourceId: resource, cancellationToken: requestToken)
				.ConfigureAwait(continueOnCapturedContext: false);

			await _httpClientAccess
				.WaitAsync(cancellationToken: requestToken)
				.ConfigureAwait(continueOnCapturedContext: false);
			try
			{
				var response = await _httpClient
					.SendAsync(
						request: request,
						completionOption: HttpCompletionOption.ResponseContentRead,
						cancellationToken: requestToken
					)
					.ConfigureAwait(continueOnCapturedContext: false);

				var bucketResponse = ParseBucketResponse(response: response);
				LogBucketResponse(resource: resource, response: response, bucketResponse: bucketResponse);

				var isRateLimitExceeded = response.StatusCode == HttpStatusCode.TooManyRequests;
				if (!isRateLimitExceeded)
				{
					_resourceManager.UpdateResource(
						resourceId: resource,
						bucketResponse: bucketResponse,
						rateLimitResponse: null
					);
					return response;
				}

				var rateLimitScope = ParseRateLimitScope(response: response);
				var rateLimitResponse = await ParseRateLimitResponseAsync(response: response)
					.ConfigureAwait(continueOnCapturedContext: false);
				_resourceManager.UpdateResource(
					resourceId: resource,
					bucketResponse: bucketResponse,
					rateLimitResponse: rateLimitResponse
				);

				var rateLimitEventArgs = new DiscordRateLimitExceeded(
					resource: resource.ToString(),
					bucketResponse: bucketResponse,
					scope: rateLimitScope,
					rateLimitResponse: rateLimitResponse,
					cancellationTokenSource: requestCancellationTokenSource
				);
				RateLimitExceeded?.Invoke(sender: this, e: rateLimitEventArgs);
			}
			finally
			{
				_httpClientAccess.Release();
			}
		}

		throw new OperationCanceledException();
	}
}