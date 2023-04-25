// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberRemove.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Gateway.Events;

public record DiscordGuildMemberRemove
{
	internal DiscordGuildMemberRemove(DiscordGuildMemberRemoveDto dto)
	{
		GuildId = ulong.Parse(s: dto.GuildId);
		User = new DiscordUser(dto: dto.User);
	}

	public ulong GuildId { get; init; }

	public DiscordUser User { get; init; }
}