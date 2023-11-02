// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageSelectMenuOptionDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

// https://discord.com/developers/docs/interactions/message-components#select-menu-object-select-option-structure
internal record DiscordMessageSelectMenuOptionDto(
	[property: JsonPropertyName(name: "label")]
	string Label,
	[property: JsonPropertyName(name: "value")]
	string Value,
	[property: JsonPropertyName(name: "description")]
	string? Description,
	[property: JsonPropertyName(name: "emoji")]
	DiscordEmojiDto? Emoji,
	[property: JsonPropertyName(name: "default")]
	bool? Default
)
{
	internal DiscordMessageSelectMenuOptionDto(DiscordMessageSelectMenuOption model) : this(
		Label: model.Label,
		Value: model.Value,
		Description: model.Description,
		Emoji: model.Emoji != null
			? new DiscordEmojiDto(model: model.Emoji)
			: null,
		Default: model.Default)
	{
	}
}