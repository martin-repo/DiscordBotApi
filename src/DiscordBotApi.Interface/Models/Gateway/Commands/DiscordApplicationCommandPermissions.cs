// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandPermissions.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Gateway.Commands;

public sealed class DiscordApplicationCommandPermissions
{
	public required ulong Id { get; init; }

	public required bool Permission { get; init; }

	public required DiscordApplicationCommandPermissionType Type { get; init; }

	public static ulong GetAllChannelsId(ulong guildId) => guildId - 1;

	public static ulong GetEveryoneId(ulong guildId) => guildId;
}