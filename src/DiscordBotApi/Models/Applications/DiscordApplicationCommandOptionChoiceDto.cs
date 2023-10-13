// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOptionChoiceDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Applications;

// https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-choice-structure
internal record DiscordApplicationCommandOptionChoiceDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "value")]
	object Value
)
{
	internal DiscordApplicationCommandOptionChoiceDto(DiscordApplicationCommandOptionChoice model) : this(
		Name: model.Name,
		Value: model.Value)
	{
	}
}