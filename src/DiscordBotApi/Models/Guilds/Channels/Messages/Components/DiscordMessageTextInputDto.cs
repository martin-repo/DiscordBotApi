// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageTextInputDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

// https://discord.com/developers/docs/interactions/message-components#text-input-object-text-input-structure
internal record DiscordMessageTextInputDto(
	[property: JsonPropertyName(name: "custom_id")]
	string CustomId,
	[property: JsonPropertyName(name: "style")]
	int Style,
	[property: JsonPropertyName(name: "label")]
	string Label,
	[property: JsonPropertyName(name: "min_length")]
	int? MinLength,
	[property: JsonPropertyName(name: "max_length")]
	int? MaxLength,
	[property: JsonPropertyName(name: "required")]
	bool? Required,
	[property: JsonPropertyName(name: "value")]
	string? Value,
	[property: JsonPropertyName(name: "placeholder")]
	string? Placeholder
) : DiscordMessageComponentDto(Type: (int)DiscordMessageComponentType.TextInput)
{
	internal DiscordMessageTextInputDto(DiscordMessageTextInput model) : this(
		CustomId: model.CustomId,
		Style: (int)model.Style,
		Label: model.Label,
		MinLength: model.MinLength,
		MaxLength: model.MaxLength,
		Required: model.Required,
		Value: model.Value,
		Placeholder: model.Placeholder)
	{
	}
}