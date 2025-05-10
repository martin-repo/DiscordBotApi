// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThreadResponse.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

public sealed class DiscordThreadResponse
{
	public required bool HasMore { get; init; }

	public required IReadOnlyCollection<DiscordChannel> Threads { get; init; }
}