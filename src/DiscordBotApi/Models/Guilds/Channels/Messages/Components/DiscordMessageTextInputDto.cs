// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageTextInputDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

// https://discord.com/developers/docs/interactions/message-components#text-input-object-text-input-structure
internal sealed record DiscordMessageTextInputDto(
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
	public static DiscordMessageTextInputDto FromModel(DiscordMessageTextInput model) =>
		new(
			CustomId: model.CustomId,
			Style: (int)model.Style,
			Label: model.Label,
			MinLength: model.MinLength,
			MaxLength: model.MaxLength,
			Required: model.Required,
			Value: model.Value,
			Placeholder: model.Placeholder
		);

	public override DiscordMessageTextInput ToModel() =>
		new()
		{
			CustomId = CustomId,
			Style = (DiscordMessageTextInputStyle)Style,
			Label = Label,
			MinLength = MinLength,
			MaxLength = MaxLength,
			Required = Required,
			Value = Value,
			Placeholder = Placeholder
		};
}