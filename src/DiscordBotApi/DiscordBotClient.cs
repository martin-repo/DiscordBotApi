// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClient.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi
{
    using DiscordBotApi.Gateway;
    using DiscordBotApi.Rest;

    using Serilog;

    public partial class DiscordBotClient : IAsyncDisposable
    {
        // https://discord.com/developers/docs/reference
        public const string DiscordApiVersion = "9";

        private const string DiscordApiBaseUrl = "https://discord.com/api/v" + DiscordApiVersion + "/";
        private const int MaxRequestsPerSecond = 50;

        private readonly DiscordRestClient _restClient;

        public DiscordBotClient(string botToken, ILogger? logger = null)
        {
            var webSocketActivator = new Func<IBinaryWebSocket>(() => new BinaryWebSocket());
            var zlibContextActivator = new Func<IZlibContext>(() => new ZlibContext());
            _gatewayClient = new DiscordGatewayClient(logger, webSocketActivator, zlibContextActivator, botToken);
            _gatewayClient.GatewayDispatchReceived += (_, eventArgs) => HandleGatewayDispatch(eventArgs.EventType, eventArgs.EventDataJson);
            _gatewayClient.GatewayDisconnected += (_, eventArgs) => GatewayDisconnected?.Invoke(this, eventArgs);
            _gatewayClient.GatewayException += (_, eventArgs) => GatewayException?.Invoke(this, eventArgs);

            var globalManager = new DiscordGlobalManager(MaxRequestsPerSecond, logger);
            globalManager.Start();

            var resourceManager = new DiscordResourceManager(logger);
            resourceManager.Start();

            _restClient = new DiscordRestClient(
                logger,
                new HttpClient(),
                globalManager,
                resourceManager,
                DiscordApiBaseUrl,
                botToken);
            _restClient.RateLimitExceeded += (_, args) => RestRateLimitExceeded?.Invoke(this, args);
        }

        public event EventHandler<DiscordRateLimitExceeded>? RestRateLimitExceeded;

        public async ValueTask DisposeAsync()
        {
            await _gatewayClient.DisposeAsync().ConfigureAwait(false);
            _restClient.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}