// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEmojiDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds.Emojis;

internal sealed record DiscordEmojiDto(
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
	public static DiscordEmojiDto FromModel(DiscordEmoji model) =>
		new(
			Id: model.Id?.ToString(),
			Name: model.Name,
			RequireColons: model.RequireColons,
			Animated: model.Animated,
			Available: model.Available
		);

	public DiscordEmoji ToModel() =>
		new()
		{
			Id = Id != null ? ulong.Parse(s: Id) : null,
			Name = Name,
			RequireColons = RequireColons,
			Animated = Animated,
			Available = Available
		};
}