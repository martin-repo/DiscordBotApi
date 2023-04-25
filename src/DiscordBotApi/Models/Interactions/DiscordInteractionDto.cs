// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Interactions;

// https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-structure
internal record DiscordInteractionDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "application_id")]
	string ApplicationId,
	[property: JsonPropertyName(name: "type")]
	int Type,
	[property: JsonPropertyName(name: "data")]
	DiscordInteractionDataDto? Data,
	[property: JsonPropertyName(name: "guild_id")]
	string? GuildId,
	[property: JsonPropertyName(name: "channel_id")]
	string? ChannelId,
	[property: JsonPropertyName(name: "member")]
	DiscordGuildMemberDto? Member,
	[property: JsonPropertyName(name: "user")]
	DiscordUserDto? User,
	[property: JsonPropertyName(name: "token")]
	string Token,
	[property: JsonPropertyName(name: "message")]
	DiscordMessageDto? Message
);