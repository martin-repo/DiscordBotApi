// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildRoleDelete.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Gateway.Events;

public sealed class DiscordGuildRoleDelete
{
	public required ulong GuildId { get; init; }

	public required ulong RoleId { get; init; }
}