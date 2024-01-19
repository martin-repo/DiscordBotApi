// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReactionAddDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds;
using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway-events#message-reaction-add-message-reaction-add-event-fields
internal record DiscordMessageReactionAddDto(
	[property: JsonPropertyName(name: "user_id")]
	string UserId,
	[property: JsonPropertyName(name: "channel_id")]
	string ChannelId,
	[property: JsonPropertyName(name: "message_id")]
	string MessageId,
	[property: JsonPropertyName(name: "guild_id")]
	string? GuildId,
	[property: JsonPropertyName(name: "member")]
	DiscordGuildMemberDto? Member,
	[property: JsonPropertyName(name: "emoji")]
	DiscordEmojiDto Emoji,
	[property: JsonPropertyName(name: "message_author_id")]
	string? MessageAuthorId
);