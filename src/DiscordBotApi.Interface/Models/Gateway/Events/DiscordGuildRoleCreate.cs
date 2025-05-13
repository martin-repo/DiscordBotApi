// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildRoleCreate.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds;

namespace DiscordBotApi.Interface.Models.Gateway.Events;

public sealed class DiscordGuildRoleCreate
{
	public required ulong GuildId { get; init; }

	public required DiscordRole Role { get; init; }
}