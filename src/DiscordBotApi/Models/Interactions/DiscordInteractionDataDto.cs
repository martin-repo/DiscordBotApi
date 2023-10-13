// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionDataDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Applications;

namespace DiscordBotApi.Models.Interactions;

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
	int? ComponentType
);