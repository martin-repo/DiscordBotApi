// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildRoleCreateDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#guild-role-create-guild-role-create-event-fields
internal record DiscordGuildRoleCreateDto(
	[property: JsonPropertyName(name: "guild_id")]
	string GuildId,
	[property: JsonPropertyName(name: "role")]
	DiscordRoleDto Role
);