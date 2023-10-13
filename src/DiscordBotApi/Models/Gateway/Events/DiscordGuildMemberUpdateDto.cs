// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberUpdateDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#guild-member-update-guild-member-update-event-fields
internal record DiscordGuildMemberUpdateDto(
	[property: JsonPropertyName(name: "guild_id")]
	string GuildId,
	[property: JsonPropertyName(name: "roles")]
	string[] Roles,
	[property: JsonPropertyName(name: "user")]
	DiscordUserDto User,
	[property: JsonPropertyName(name: "nick")]
	string? Nick
);