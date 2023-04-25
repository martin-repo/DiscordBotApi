// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberAdd.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds;

namespace DiscordBotApi.Models.Gateway.Events;

public record DiscordGuildMemberAdd : DiscordGuildMember
{
	internal DiscordGuildMemberAdd(DiscordGuildMemberAddDto dto) : base(dto: dto)
	{
		GuildId = ulong.Parse(s: dto.GuildId);
	}

	public ulong GuildId { get; init; }
}