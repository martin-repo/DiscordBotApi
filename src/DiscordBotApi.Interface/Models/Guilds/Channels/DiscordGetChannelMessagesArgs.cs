// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGetChannelMessagesArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

public sealed class DiscordGetChannelMessagesArgs
{
	public ulong? After { get; init; }

	public ulong? Around { get; init; }

	public ulong? Before { get; init; }

	public int? Limit { get; init; }
}