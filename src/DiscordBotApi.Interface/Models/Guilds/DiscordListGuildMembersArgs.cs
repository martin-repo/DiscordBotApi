// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordListGuildMembersArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds;

public sealed class DiscordListGuildMembersArgs
{
	public ulong? After { get; init; }

	public int? Limit { get; init; }
}