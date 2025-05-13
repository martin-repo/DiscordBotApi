// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMember.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Users;

namespace DiscordBotApi.Interface.Models.Guilds;

public class DiscordGuildMember
{
	public string? Nick { get; init; }

	public DiscordPermissions? Permissions { get; init; }

	public required IReadOnlyCollection<ulong> Roles { get; init; }

	public DiscordUser? User { get; init; }
}