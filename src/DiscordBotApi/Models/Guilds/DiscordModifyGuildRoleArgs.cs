﻿// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyGuildRoleArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds;

public record DiscordModifyGuildRoleArgs
{
	public string? Name { get; init; }

	public DiscordPermissions? Permissions { get; init; }
}