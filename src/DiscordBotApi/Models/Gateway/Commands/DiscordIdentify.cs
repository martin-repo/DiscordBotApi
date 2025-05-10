// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordIdentify.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Gateway.Commands;

namespace DiscordBotApi.Models.Gateway.Commands;

internal sealed record DiscordIdentify(
	string Token,
	DiscordGatewayConnectionProperties Properties,
	DiscordShard? Shard,
	int Intents
);