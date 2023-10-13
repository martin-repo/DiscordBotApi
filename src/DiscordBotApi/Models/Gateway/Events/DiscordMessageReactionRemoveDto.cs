// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReactionRemoveDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#message-reaction-remove-message-reaction-remove-event-fields
internal record DiscordMessageReactionRemoveDto(
	[property: JsonPropertyName(name: "user_id")]
	string UserId,
	[property: JsonPropertyName(name: "channel_id")]
	string ChannelId,
	[property: JsonPropertyName(name: "message_id")]
	string MessageId,
	[property: JsonPropertyName(name: "guild_id")]
	string? GuildId,
	[property: JsonPropertyName(name: "emoji")]
	DiscordEmojiDto Emoji
);