// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPermissionOverwrite.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds;

public sealed class DiscordPermissionOverwrite
{
	public required DiscordPermissions Allow { get; init; }

	public required DiscordPermissions Deny { get; init; }

	public required ulong Id { get; init; }

	public required DiscordPermissionOverwriteType Type { get; init; }
}