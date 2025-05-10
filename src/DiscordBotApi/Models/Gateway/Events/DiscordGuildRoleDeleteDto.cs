// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildRoleDeleteDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Events;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#guild-role-delete-guild-role-delete-event-fields
internal sealed record DiscordGuildRoleDeleteDto(
	[property: JsonPropertyName(name: "guild_id")]
	string GuildId,
	[property: JsonPropertyName(name: "role_id")]
	string RoleId
)
{
	public DiscordGuildRoleDelete ToModel() =>
		new()
		{
			GuildId = ulong.Parse(s: GuildId),
			RoleId = ulong.Parse(s: RoleId)
		};
}