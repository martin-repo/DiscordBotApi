// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildApplicationCommandPermissions.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands;

public record DiscordGuildApplicationCommandPermissions
{
	internal DiscordGuildApplicationCommandPermissions(DiscordGuildApplicationCommandPermissionsDto dto)
	{
		Id = ulong.Parse(s: dto.Id);
		ApplicationId = ulong.Parse(s: dto.ApplicationId);
		GuildId = ulong.Parse(s: dto.GuildId);
		Permissions = dto.Permissions.Select(selector: p => new DiscordApplicationCommandPermissions(dto: p))
			.ToArray();
	}

	public ulong ApplicationId { get; init; }

	public ulong GuildId { get; init; }

	public ulong Id { get; init; }

	public IReadOnlyCollection<DiscordApplicationCommandPermissions> Permissions { get; init; }
}