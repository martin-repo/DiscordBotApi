// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildRoleCreateDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Events;
using DiscordBotApi.Models.Guilds;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#guild-role-create-guild-role-create-event-fields
internal sealed record DiscordGuildRoleCreateDto(
	[property: JsonPropertyName(name: "guild_id")]
	string GuildId,
	[property: JsonPropertyName(name: "role")]
	DiscordRoleDto Role
)

{
	public DiscordGuildRoleCreate ToModel() =>
		new()
		{
			GuildId = ulong.Parse(s: GuildId),
			Role = Role.ToModel()
		};
}