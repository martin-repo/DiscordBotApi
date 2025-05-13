// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildEmojiArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds;

internal sealed record DiscordCreateGuildEmojiArgsDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "image")]
	string Image
);