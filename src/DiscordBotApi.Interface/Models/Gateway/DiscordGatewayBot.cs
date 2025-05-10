// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayBot.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Gateway;

public sealed class DiscordGatewayBot
{
	public required DiscordSessionStartLimit SessionStartLimit { get; init; }

	public required int Shards { get; init; }

	public required string Url { get; init; }
}