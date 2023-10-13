// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberRemoveDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#guild-member-remove-guild-member-remove-event-fields
internal record DiscordGuildMemberRemoveDto(
	[property: JsonPropertyName(name: "guild_id")]
	string GuildId,
	[property: JsonPropertyName(name: "user")]
	DiscordUserDto User
);