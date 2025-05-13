// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClient.Gateway.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Gateway;
using DiscordBotApi.Interface.Gateway;
using DiscordBotApi.Interface.Models.Gateway;
using DiscordBotApi.Interface.Models.Gateway.Commands;
using DiscordBotApi.Interface.Models.Gateway.Events;
using DiscordBotApi.Models.Gateway;

namespace DiscordBotApi;

public partial class DiscordBotClient
{
	private readonly DiscordGatewayClient _gatewayClient;

	public event EventHandler<DiscordGatewayException>? GatewayException;

	public event EventHandler<DiscordReady>? GatewayReady;

	public async Task<DiscordReady> ConnectToGatewayAsync(
		string gatewayUrl,
		DiscordGatewayIntent intents,
		DiscordShard? shard = null,
		CancellationToken cancellationToken = default
	)
	{
		var gatewayReady =
			new TaskCompletionSource<DiscordReady>(creationOptions: TaskCreationOptions.RunContinuationsAsynchronously);

		void OnGatewayReady(object? sender, DiscordReady ready)
		{
			gatewayReady.SetResult(result: ready);
			GatewayReady?.Invoke(sender: this, e: ready);
		}

		_gatewayClient.GatewayReady += OnGatewayReady;
		try
		{
			await _gatewayClient
				.ConnectAsync(
					gatewayUrl: gatewayUrl,
					intents: intents,
					shard: shard,
					cancellationToken: cancellationToken
				)
				.ConfigureAwait(continueOnCapturedContext: false);
			return await gatewayReady.Task.ConfigureAwait(continueOnCapturedContext: false);
		}
		finally
		{
			_gatewayClient.GatewayReady -= OnGatewayReady;
		}
	}

	public async Task DisconnectFromGatewayAsync(
		DiscordGatewayCloseType closeType = DiscordGatewayCloseType.NormalClosure
	) =>
		await _gatewayClient.DisconnectAsync(closeType: closeType).ConfigureAwait(continueOnCapturedContext: false);

	// https://discord.com/developers/docs/topics/gateway#get-gateway
	public async Task<DiscordGateway> GetGatewayAsync(CancellationToken cancellationToken = default)
	{
		const string Url = "gateway";

		var gatewayDto = await _restClient
			.SendRequestAsync<DiscordGatewayDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: Url),
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);

		var gateway = gatewayDto.ToModel();

		return gateway;
	}

	// https://discord.com/developers/docs/topics/gateway#get-gateway-bot
	public async Task<DiscordGatewayBot> GetGatewayBotAsync(CancellationToken cancellationToken = default)
	{
		const string Url = "gateway/bot";

		var gatewayBotDto = await _restClient
			.SendRequestAsync<DiscordGatewayBotDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: Url),
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);

		var gatewayBot = gatewayBotDto.ToModel();

		return gatewayBot;
	}

	public async Task UpdatePresenceAsync(
		DiscordPresenceUpdate presenceUpdate,
		CancellationToken cancellationToken = default
	) =>
		await _gatewayClient
			.UpdatePresenceAsync(presenceUpdate: presenceUpdate, cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
}