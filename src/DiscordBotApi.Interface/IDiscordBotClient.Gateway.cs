// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordBotClient.Gateway.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Gateway;
using DiscordBotApi.Interface.Models.Gateway;
using DiscordBotApi.Interface.Models.Gateway.Commands;
using DiscordBotApi.Interface.Models.Gateway.Events;

namespace DiscordBotApi.Interface;

public partial interface IDiscordBotClient
{
	event EventHandler<DiscordGatewayException>? GatewayException;

	event EventHandler<DiscordReady>? GatewayReady;

	Task<DiscordReady> ConnectToGatewayAsync(
		string gatewayUrl,
		DiscordGatewayIntent intents,
		DiscordShard? shard = null,
		CancellationToken cancellationToken = default
	);

	Task<DiscordGateway> GetGatewayAsync(CancellationToken cancellationToken = default);

	Task UpdatePresenceAsync(DiscordPresenceUpdate presenceUpdate, CancellationToken cancellationToken = default);
}