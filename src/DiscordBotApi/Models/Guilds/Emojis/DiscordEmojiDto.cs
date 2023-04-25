// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEmojiDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Emojis;

internal record DiscordEmojiDto(
	[property: JsonPropertyName(name: "id")]
	string? Id,
	[property: JsonPropertyName(name: "name")]
	string? Name,
	[property: JsonPropertyName(name: "require_colons")]
	bool? RequireColons,
	[property: JsonPropertyName(name: "animated")]
	bool? Animated,
	[property: JsonPropertyName(name: "available")]
	bool? Available
)
{
	internal DiscordEmojiDto(DiscordEmoji model) : this(
		Id: model.Id?.ToString(),
		Name: model.Name,
		RequireColons: model.RequireColons,
		Animated: model.Animated,
		Available: model.Available)
	{
	}
}