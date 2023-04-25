// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClient.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Gateway;
using DiscordBotApi.Rest;

using Serilog;

namespace DiscordBotApi;

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
		_gatewayClient = new DiscordGatewayClient(
			logger: logger,
			webSocketActivator: webSocketActivator,
			zlibContextActivator: zlibContextActivator,
			botToken: botToken);
		_gatewayClient.GatewayDispatchReceived += (_, eventArgs) =>
			HandleGatewayDispatch(eventType: eventArgs.EventType, eventDataJson: eventArgs.EventDataJson);
		_gatewayClient.GatewayException += (_, eventArgs) => GatewayException?.Invoke(sender: this, e: eventArgs);

		var globalManager = new DiscordGlobalManager(globalLimit: MaxRequestsPerSecond, logger: logger);
		globalManager.Start();

		var resourceManager = new DiscordResourceManager(logger: logger);
		resourceManager.Start();

		_restClient = new DiscordRestClient(
			logger: logger,
			httpClient: new HttpClient(),
			globalManager: globalManager,
			resourceManager: resourceManager,
			baseUrl: DiscordApiBaseUrl,
			botToken: botToken);
		_restClient.RateLimitExceeded += (_, args) => RestRateLimitExceeded?.Invoke(sender: this, e: args);
	}

	public event EventHandler<DiscordRateLimitExceeded>? RestRateLimitExceeded;

	public async ValueTask DisposeAsync()
	{
		await _gatewayClient.DisposeAsync()
			.ConfigureAwait(continueOnCapturedContext: false);
		_restClient.Dispose();

		GC.SuppressFinalize(obj: this);
	}
}