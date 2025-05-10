// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordActiveThreadResponse.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#list-public-archived-threads-response-body
public sealed class DiscordActiveThreadResponse
{
	public required IReadOnlyCollection<DiscordChannel> Threads { get; init; }
}