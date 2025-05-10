// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildRoleArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds;

public sealed class DiscordCreateGuildRoleArgs
{
	public string? Name { get; init; }

	public DiscordPermissions? Permissions { get; init; }
}