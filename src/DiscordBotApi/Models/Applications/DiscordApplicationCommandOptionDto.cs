// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOptionDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Applications;

// https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-structure
internal record DiscordApplicationCommandOptionDto(
	[property: JsonPropertyName(name: "type")]
	int Type,
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "description")]
	string Description,
	[property: JsonPropertyName(name: "required")]
	bool? Required,
	[property: JsonPropertyName(name: "choices")]
	DiscordApplicationCommandOptionChoiceDto[]? Choices,
	[property: JsonPropertyName(name: "options")]
	DiscordApplicationCommandOptionDto[]? Options,
	[property: JsonPropertyName(name: "channel_types")]
	int[]? ChannelTypes,
	[property: JsonPropertyName(name: "min_value")]
	object? MinValue,
	[property: JsonPropertyName(name: "max_value")]
	object? MaxValue,
	[property: JsonPropertyName(name: "autocomplete")]
	bool? Autocomplete
)
{
	internal DiscordApplicationCommandOptionDto(DiscordApplicationCommandOption model) : this(
		Type: (int)model.Type,
		Name: model.Name,
		Description: model.Description,
		Required: model.Required,
		Choices: model.Choices?.Select(selector: c => new DiscordApplicationCommandOptionChoiceDto(model: c))
			.ToArray(),
		Options: model.Options?.Select(selector: o => new DiscordApplicationCommandOptionDto(model: o))
			.ToArray(),
		ChannelTypes: model.ChannelTypes?.Select(selector: t => (int)t)
			.ToArray(),
		MinValue: model.MinValue,
		MaxValue: model.MaxValue,
		Autocomplete: model.Autocomplete)
	{
	}
}