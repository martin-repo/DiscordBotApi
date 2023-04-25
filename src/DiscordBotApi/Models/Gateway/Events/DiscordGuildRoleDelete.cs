// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildRoleDelete.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events;

public record DiscordGuildRoleDelete
{
	internal DiscordGuildRoleDelete(DiscordGuildRoleDeleteDto dto)
	{
		GuildId = ulong.Parse(s: dto.GuildId);
		RoleId = ulong.Parse(s: dto.RoleId);
	}

	public ulong GuildId { get; init; }

	public ulong RoleId { get; init; }
}