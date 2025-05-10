// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRole.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds;

public sealed class DiscordRole
{
	public required ulong Id { get; init; }

	public required string Name { get; init; }

	public required DiscordPermissions Permissions { get; init; }
}