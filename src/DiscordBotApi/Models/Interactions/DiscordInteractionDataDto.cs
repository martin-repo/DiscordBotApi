// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionDataDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Applications;
using DiscordBotApi.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Models.Interactions;

// https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-application-command-data-structure
internal record DiscordInteractionDataDto(
	[property: JsonPropertyName(name: "id")]
	string? Id,
	[property: JsonPropertyName(name: "name")]
	string? Name,
	[property: JsonPropertyName(name: "type")]
	int? Type,
	[property: JsonPropertyName(name: "options")]
	DiscordApplicationCommandInteractionDataOptionDto[]? Options,
	[property: JsonPropertyName(name: "custom_id")]
	string? CustomId,
	[property: JsonPropertyName(name: "component_type")]
	int? ComponentType,
	[property: JsonPropertyName(name: "values")]
	string[]? Values,
	[property: JsonPropertyName(name: "components")]
	DiscordMessageComponentDto[]? Components
);