// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildApplicationCommandPermissions.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Gateway.Commands;

public sealed class DiscordGuildApplicationCommandPermissions
{
	public required ulong ApplicationId { get; init; }

	public required ulong GuildId { get; init; }

	public required ulong Id { get; init; }

	public required IReadOnlyCollection<DiscordApplicationCommandPermissions> Permissions { get; init; }
}