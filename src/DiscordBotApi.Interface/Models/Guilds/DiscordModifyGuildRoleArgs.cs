// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyGuildRoleArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds;

public sealed class DiscordModifyGuildRoleArgs
{
	public string? Name { get; init; }

	public DiscordPermissions? Permissions { get; init; }
}