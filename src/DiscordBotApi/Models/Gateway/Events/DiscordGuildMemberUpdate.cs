// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberUpdate.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Gateway.Events;

public record DiscordGuildMemberUpdate
{
	internal DiscordGuildMemberUpdate(DiscordGuildMemberUpdateDto dto)
	{
		GuildId = ulong.Parse(s: dto.GuildId);
		Roles = dto.Roles.Select(selector: ulong.Parse)
			.ToArray();
		User = new DiscordUser(dto: dto.User);
		Nick = dto.Nick;
	}

	public ulong GuildId { get; init; }

	public string? Nick { get; init; }

	public IReadOnlyCollection<ulong> Roles { get; init; }

	public DiscordUser User { get; init; }
}