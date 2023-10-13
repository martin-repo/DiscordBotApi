// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMember.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Guilds;

public record DiscordGuildMember
{
	internal DiscordGuildMember(DiscordGuildMemberDto dto)
	{
		User = dto.User != null
			? new DiscordUser(dto: dto.User)
			: null;
		Nick = dto.Nick;
		Roles = dto.Roles.Select(selector: ulong.Parse)
			.ToArray();
	}

	public string? Nick { get; init; }

	public IReadOnlyCollection<ulong> Roles { get; init; }

	public DiscordUser? User { get; init; }
}