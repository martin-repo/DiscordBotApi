// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Guilds;

// https://discord.com/developers/docs/resources/guild#guild-member-object
internal class DiscordGuildMemberDto
{
	[JsonPropertyName(name: "nick")]
	public string? Nick { get; init; }

	[JsonPropertyName(name: "permissions")]
	public string? Permissions { get; init; }

	[JsonPropertyName(name: "roles")]
	public required ImmutableArray<string> Roles { get; init; }

	[JsonPropertyName(name: "user")]
	public DiscordUserDto? User { get; init; }

	public virtual DiscordGuildMember ToModel() =>
		new()
		{
			User = User?.ToModel(),
			Nick = Nick,
			Roles = Roles.Select(selector: ulong.Parse).ToImmutableArray(),
			Permissions = Permissions is not null ? (DiscordPermissions)ulong.Parse(s: Permissions) : null
		};
}