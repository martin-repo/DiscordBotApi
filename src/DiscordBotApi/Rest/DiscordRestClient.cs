// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRestClient.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Rest
{
    using System.Globalization;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Rest;

    using Serilog;

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
        private readonly SemaphoreSlim _httpClientAccess = new(MaxSimultaneousConnections, MaxSimultaneousConnections);
        private readonly ILogger? _logger;
        private readonly IDiscordResourceManager _resourceManager;

        private long _requestIndex;

        public DiscordRestClient(
            ILogger? logger,
            HttpClient httpClient,
            IDiscordGlobalManager globalManager,
            IDiscordResourceManager resourceManager,
            string baseUrl,
            string botToken)
        {
            _logger = logger?.ForContext<DiscordRestClient>();

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(baseUrl);

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            if (version == null)
            {
                throw new InvalidOperationException("Failed to extract assembly version.");
            }

            _logger?.Debug("{Name} {Version}", nameof(DiscordBotClient), version);

            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(nameof(DiscordBotClient), version.ToString(3)));
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bot {botToken}");

            _globalManager = globalManager;
            _resourceManager = resourceManager;
        }

        ~DiscordRestClient()
        {
            Dispose(false);
        }

        public event EventHandler<DiscordRateLimitExceeded>? RateLimitExceeded;

        public HttpContent CreateJsonContent(object value)
        {
            var json = JsonSerializer.Serialize(value, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            var jsonContent = new StringContent(json, Encoding.UTF8, JsonContentType);
            return jsonContent;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SendRequestAsync(
            Func<HttpRequestMessage> requestFactoryFunc,
            HttpStatusCode expectedResponseCode,
            CancellationToken cancellationToken)
        {
            using var response = await SendAndWaitIfRateLimitExceededAsync(requestFactoryFunc, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode != expectedResponseCode)
            {
                var contentJson = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                var errorResponse = ParseDiscordErrorResponse(contentJson);
                throw new DiscordRestException(response.StatusCode, errorResponse);
            }
        }

        public async Task<T> SendRequestAsync<T>(Func<HttpRequestMessage> requestFactoryFunc, CancellationToken cancellationToken)
            where T : class
        {
            using var response = await SendAndWaitIfRateLimitExceededAsync(requestFactoryFunc, cancellationToken).ConfigureAwait(false);
            var contentJson = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = ParseDiscordErrorResponse(contentJson);
                throw new DiscordRestException(response.StatusCode, errorResponse);
            }

            var content = JsonSerializer.Deserialize<T>(contentJson);
            if (content == null)
            {
                throw new SerializationException("Failed to deserialize Json.");
            }

            return content;
        }

        private static string? GetHeaderValue(HttpResponseHeaders headers, string key)
        {
            if (!headers.TryGetValues(key, out var values))
            {
                return null;
            }

            var valueArray = values.ToArray();
            return valueArray.Length == 1 ? valueArray[0] : throw new InvalidOperationException($"Headers contains multiple values for {key}");
        }

        private static DiscordErrorResponse ParseDiscordErrorResponse(string json)
        {
            DiscordErrorResponseDto? errorResponseDto;
            try
            {
                errorResponseDto = JsonSerializer.Deserialize<DiscordErrorResponseDto>(json);
            }
            catch (JsonException)
            {
                return new DiscordErrorResponse(DiscordJsonErrorCode.GeneralError, "Invalid error response", json);
            }

            if (errorResponseDto == null)
            {
                return new DiscordErrorResponse(DiscordJsonErrorCode.GeneralError, "Unknown error", json);
            }

            var errorResponse = new DiscordErrorResponse(errorResponseDto);
            return errorResponse;
        }

        private static async Task<DiscordRateLimitResponse> ParseRateLimitResponseAsync(HttpResponseMessage response)
        {
            var contentJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var responseDto = JsonSerializer.Deserialize<DiscordRateLimitResponseDto>(contentJson)!;
            return new DiscordRateLimitResponse(responseDto);
        }

        private static DiscordRateLimitScope ParseRateLimitScope(HttpResponseMessage response)
        {
            var scopeString = GetHeaderValue(response.Headers, RateLimitScope);
            switch (scopeString)
            {
                case "user":
                    return DiscordRateLimitScope.User;
                case "global":
                    return DiscordRateLimitScope.Global;
                case "shared":
                    return DiscordRateLimitScope.Shared;
                default:
                    throw new NotSupportedException($"{scopeString} is not a valid {nameof(DiscordRateLimitScope)}");
            }
        }

        private void Dispose(bool disposeManaged)
        {
            if (disposeManaged)
            {
                _httpClient.Dispose();
            }
        }

        private void LogBucketResponse(DiscordResourceId resource, HttpResponseMessage response, DiscordBucketResponse? bucketResponse)
        {
            if (bucketResponse != null)
            {
                _logger?.Debug(
                    "{Code} {Bucket} {Remaining}/{Limit} {ResetAfter} - {Resource}",
                    (int)response.StatusCode,
                    bucketResponse.Bucket,
                    bucketResponse.Remaining,
                    bucketResponse.Limit,
                    bucketResponse.ResetAfter.TotalSeconds + "s",
                    resource);
            }
            else
            {
                _logger?.Debug("{Code} - {Resource}", (int)response.StatusCode, resource);
            }
        }

        private DiscordBucketResponse? ParseBucketResponse(HttpResponseMessage response)
        {
            var headers = response.Headers;

            var bucket = GetHeaderValue(headers, RateLimitBucket);
            if (bucket == null)
            {
                return null;
            }

            var limitValue = GetHeaderValue(headers, RateLimitLimit);
            var limit = int.Parse(limitValue!);

            var remainingValue = GetHeaderValue(headers, RateLimitRemaining);
            var remaining = int.Parse(remainingValue!);

            var resetValue = GetHeaderValue(headers, RateLimitReset);
            var resetSeconds = double.Parse(resetValue!, CultureInfo.InvariantCulture);

            var resetAfterValue = GetHeaderValue(headers, RateLimitResetAfter);
            var resetAfterSeconds = double.Parse(resetAfterValue!, CultureInfo.InvariantCulture);
            var resetAfter = TimeSpan.FromSeconds(resetAfterSeconds);

            return new(bucket, limit, remaining, resetSeconds, resetAfter);
        }

        private async Task<HttpResponseMessage> SendAndWaitIfRateLimitExceededAsync(
            Func<HttpRequestMessage> requestFactoryFunc,
            CancellationToken cancellationToken)
        {
            using var requestCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var requestToken = requestCancellationTokenSource.Token;

            var requestIndex = Interlocked.Increment(ref _requestIndex);
            while (!requestToken.IsCancellationRequested)
            {
                var request = requestFactoryFunc();
                var resource = _resourceManager.GetResourceId(request.Method.ToString(), request.RequestUri!.OriginalString);

                using var reservation = await _resourceManager.GetReservationAsync(resource, requestIndex, requestToken).ConfigureAwait(false);
                await _globalManager.GetReservationAsync(resource, cancellationToken).ConfigureAwait(false);

                await _httpClientAccess.WaitAsync(requestToken).ConfigureAwait(false);
                try
                {
                    var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead, requestToken).ConfigureAwait(false);

                    var bucketResponse = ParseBucketResponse(response);
                    LogBucketResponse(resource, response, bucketResponse);

                    var isRateLimitExceeded = response.StatusCode == HttpStatusCode.TooManyRequests;
                    if (!isRateLimitExceeded)
                    {
                        _resourceManager.UpdateResource(resource, bucketResponse, null);
                        return response;
                    }

                    var rateLimitScope = ParseRateLimitScope(response);
                    var rateLimitResponse = await ParseRateLimitResponseAsync(response).ConfigureAwait(false);
                    _resourceManager.UpdateResource(resource, bucketResponse, rateLimitResponse);

                    var rateLimitEventArgs = new DiscordRateLimitExceeded(
                        resource.ToString(),
                        bucketResponse,
                        rateLimitScope,
                        rateLimitResponse,
                        requestCancellationTokenSource);
                    RateLimitExceeded?.Invoke(this, rateLimitEventArgs);
                }
                finally
                {
                    _httpClientAccess.Release();
                }
            }

            throw new OperationCanceledException();
        }
    }
}