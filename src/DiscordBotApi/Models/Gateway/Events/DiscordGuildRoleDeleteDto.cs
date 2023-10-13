// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildRoleDeleteDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#guild-role-delete-guild-role-delete-event-fields
internal record DiscordGuildRoleDeleteDto(
	[property: JsonPropertyName(name: "guild_id")]
	string GuildId,
	[property: JsonPropertyName(name: "role_id")]
	string RoleId
);