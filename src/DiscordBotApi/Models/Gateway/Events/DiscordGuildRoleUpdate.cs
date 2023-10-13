// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildRoleUpdate.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds;

namespace DiscordBotApi.Models.Gateway.Events;

public record DiscordGuildRoleUpdate
{
	internal DiscordGuildRoleUpdate(DiscordBotClient botClient, DiscordGuildRoleUpdateDto dto)
	{
		GuildId = ulong.Parse(s: dto.GuildId);
		Role = new DiscordRole(botClient: botClient, guildId: ulong.Parse(s: dto.GuildId), dto: dto.Role);
	}

	public ulong GuildId { get; init; }

	public DiscordRole Role { get; init; }
}