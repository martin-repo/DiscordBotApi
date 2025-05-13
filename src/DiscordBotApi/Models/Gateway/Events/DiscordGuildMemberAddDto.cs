// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberAddDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Events;
using DiscordBotApi.Interface.Models.Guilds;
using DiscordBotApi.Models.Guilds;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/events/gateway-events#guild-member-add
internal sealed class DiscordGuildMemberAddDto : DiscordGuildMemberDto
{
	[JsonPropertyName(name: "guild_id")]
	public required string GuildId { get; init; }

	public override DiscordGuildMemberAdd ToModel() =>
		new()
		{
			GuildId = ulong.Parse(s: GuildId),
			User = User?.ToModel(),
			Nick = Nick,
			Roles = Roles.Select(selector: ulong.Parse).ToImmutableArray(),
			Permissions = Permissions is not null ? (DiscordPermissions)ulong.Parse(s: Permissions) : null
		};
}