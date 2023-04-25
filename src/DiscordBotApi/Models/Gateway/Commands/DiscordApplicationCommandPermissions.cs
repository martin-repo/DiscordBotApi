// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandPermissions.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands;

public record DiscordApplicationCommandPermissions
{
	internal DiscordApplicationCommandPermissions(DiscordApplicationCommandPermissionsDto dto)
	{
		Id = ulong.Parse(s: dto.Id);
		Type = (DiscordApplicationCommandPermissionType)dto.Type;
		Permission = dto.Permission;
	}

	public ulong Id { get; init; }

	public bool Permission { get; init; }

	public DiscordApplicationCommandPermissionType Type { get; init; }

	public static ulong GetAllChannelsId(ulong guildId) => guildId - 1;

	public static ulong GetEveryoneId(ulong guildId) => guildId;
}