// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordListGuildMembersArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds;

public record DiscordListGuildMembersArgs
{
	public ulong? After { get; init; }

	public int? Limit { get; init; }
}