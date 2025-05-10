// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordListPublicArchivedThreadsArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

public sealed class DiscordListPublicArchivedThreadsArgs
{
	public DateTime? Before { get; init; }

	public int? Limit { get; init; }
}