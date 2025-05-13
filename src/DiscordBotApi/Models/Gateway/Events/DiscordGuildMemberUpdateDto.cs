// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberUpdateDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Events;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#guild-member-update-guild-member-update-event-fields
internal sealed record DiscordGuildMemberUpdateDto(
	[property: JsonPropertyName(name: "guild_id")]
	string GuildId,
	[property: JsonPropertyName(name: "roles")]
	string[] Roles,
	[property: JsonPropertyName(name: "user")]
	DiscordUserDto User,
	[property: JsonPropertyName(name: "nick")]
	string? Nick
)
{
	public DiscordGuildMemberUpdate ToModel() =>
		new()
		{
			GuildId = ulong.Parse(s: GuildId),
			Roles = Roles.Select(selector: ulong.Parse).ToArray(),
			User = User.ToModel(),
			Nick = Nick
		};
}