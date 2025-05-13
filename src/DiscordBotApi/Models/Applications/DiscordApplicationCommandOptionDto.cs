// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOptionDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Applications;
using DiscordBotApi.Interface.Models.Guilds.Channels;
using DiscordBotApi.Utilities;

namespace DiscordBotApi.Models.Applications;

// https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-structure
internal sealed record DiscordApplicationCommandOptionDto(
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
	[property: JsonPropertyName(name: "min_length")]
	int? MinLength,
	[property: JsonPropertyName(name: "max_length")]
	int? MaxLength,
	[property: JsonPropertyName(name: "autocomplete")]
	bool? Autocomplete
)
{
	public static DiscordApplicationCommandOptionDto FromModel(DiscordApplicationCommandOption model) =>
		new(
			Type: (int)model.Type,
			Name: model.Name,
			Description: model.Description,
			Required: model.Required,
			Choices: model.Choices?.Select(selector: DiscordApplicationCommandOptionChoiceDto.FromModel).ToArray(),
			Options: model.Options?.Select(selector: FromModel).ToArray(),
			ChannelTypes: model.ChannelTypes?.Select(selector: t => (int)t).ToArray(),
			MinValue: model.MinValue,
			MaxValue: model.MaxValue,
			MinLength: model.MinLength,
			MaxLength: model.MaxLength,
			Autocomplete: model.Autocomplete
		);

	public DiscordApplicationCommandOption ToModel() =>
		new()
		{
			Type = (DiscordApplicationCommandOptionType)Type,
			Name = Name,
			Description = Description,
			Required = Required,
			Choices =
				Choices?.Select(selector: c => c.ToModel(type: (DiscordApplicationCommandOptionType)Type)).ToArray(),
			Options = Options?.Select(selector: o => o.ToModel()).ToArray(),
			ChannelTypes = ChannelTypes?.Select(selector: t => (DiscordChannelType)t).ToArray(),
			MinValue =
				MinValue != null
					? JsonParseUtils.ToObject(type: (DiscordApplicationCommandOptionType)Type, jsonValue: MinValue)
					: null,
			MaxValue =
				MaxValue != null
					? JsonParseUtils.ToObject(type: (DiscordApplicationCommandOptionType)Type, jsonValue: MaxValue)
					: null,
			MinLength = MinLength,
			MaxLength = MaxLength,
			Autocomplete = Autocomplete
		};
}