// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildApplicationCommandArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Applications;

internal record DiscordCreateGuildApplicationCommandArgsDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "description")]
	string Description,
	[property: JsonPropertyName(name: "options")]
	IReadOnlyCollection<DiscordApplicationCommandOptionDto>? Options,
	[property: JsonPropertyName(name: "type")]
	int? Type
)
{
	internal DiscordCreateGuildApplicationCommandArgsDto(DiscordCreateGuildApplicationCommandArgs model) : this(
		Name: model.Name,
		Description: model.Description,
		Options: model.Options?.Select(selector: o => new DiscordApplicationCommandOptionDto(model: o))
			.ToArray(),
		Type: model.Type != null
			? (int)model.Type
			: null)
	{
	}
}