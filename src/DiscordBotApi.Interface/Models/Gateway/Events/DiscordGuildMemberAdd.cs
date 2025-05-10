// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberAdd.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds;

namespace DiscordBotApi.Interface.Models.Gateway.Events;

public sealed class DiscordGuildMemberAdd : DiscordGuildMember
{
	public required ulong GuildId { get; init; }
}