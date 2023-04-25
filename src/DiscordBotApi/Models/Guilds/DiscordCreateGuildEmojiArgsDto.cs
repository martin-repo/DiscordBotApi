// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildEmojiArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds;

internal record DiscordCreateGuildEmojiArgsDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "image")]
	string Image
);