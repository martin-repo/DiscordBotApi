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
		User = dto.User is not null
			? new DiscordUser(dto: dto.User)
			: null;
		Nick = dto.Nick;
		Roles = dto.Roles.Select(selector: ulong.Parse)
			.ToArray();
		Permissions = dto.Permissions is not null
			? (DiscordPermissions)ulong.Parse(s: dto.Permissions)
			: null;
	}

	public string? Nick { get; init; }

	public DiscordPermissions? Permissions { get; init; }

	public IReadOnlyCollection<ulong> Roles { get; init; }

	public DiscordUser? User { get; init; }
}