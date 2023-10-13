// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandInteractionDataOptionDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Applications;

// https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-interaction-data-option-structure
internal record DiscordApplicationCommandInteractionDataOptionDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "type")]
	int Type,
	[property: JsonPropertyName(name: "value")]
	object Value,
	[property: JsonPropertyName(name: "options")]
	DiscordApplicationCommandInteractionDataOptionDto[]? Options,
	[property: JsonPropertyName(name: "focused")]
	bool? Focused
);