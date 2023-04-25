// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageButtonDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

internal record DiscordMessageButtonDto(
	[property: JsonPropertyName(name: "style")]
	int Style,
	[property: JsonPropertyName(name: "label")]
	string? Label,
	[property: JsonPropertyName(name: "emoji")]
	DiscordEmojiDto? Emoji,
	[property: JsonPropertyName(name: "custom_id")]
	string? CustomId,
	[property: JsonPropertyName(name: "url")]
	string? Url,
	[property: JsonPropertyName(name: "disabled")]
	bool? Disabled
) : DiscordMessageComponentDto(Type: (int)DiscordMessageComponentType.Button)
{
	internal DiscordMessageButtonDto(DiscordMessageButton model) : this(
		Style: (int)model.Style,
		Label: model.Label,
		Emoji: model.Emoji != null
			? new DiscordEmojiDto(model: model.Emoji)
			: null,
		CustomId: model.CustomId,
		Url: model.Url,
		Disabled: model.Disabled)
	{
	}
}