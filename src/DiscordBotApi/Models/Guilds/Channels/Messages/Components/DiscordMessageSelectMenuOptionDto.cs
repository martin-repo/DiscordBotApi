// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageSelectMenuOptionDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

// https://discord.com/developers/docs/interactions/message-components#select-menu-object-select-option-structure
internal sealed record DiscordMessageSelectMenuOptionDto(
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
	public static DiscordMessageSelectMenuOptionDto FromModel(DiscordMessageSelectMenuOption model) =>
		new(
			Label: model.Label,
			Value: model.Value,
			Description: model.Description,
			Emoji: model.Emoji != null ? DiscordEmojiDto.FromModel(model: model.Emoji) : null,
			Default: model.Default
		);

	public DiscordMessageSelectMenuOption ToModel() =>
		new()
		{
			Label = Label,
			Value = Value,
			Description = Description,
			Emoji = Emoji?.ToModel(),
			Default = Default
		};
}