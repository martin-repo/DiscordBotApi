// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordListPublicArchivedThreadsArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels;

public record DiscordListPublicArchivedThreadsArgs
{
	public DateTime? Before { get; init; }

	public int? Limit { get; init; }
}