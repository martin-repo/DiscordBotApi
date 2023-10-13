// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReactionDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

internal record DiscordReactionDto(
	[property: JsonPropertyName(name: "count")]
	int Count,
	[property: JsonPropertyName(name: "me")]
	bool Me,
	[property: JsonPropertyName(name: "emoji")]
	DiscordEmojiDto Emoji
);