// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandInteractionDataOptionDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Applications;
using DiscordBotApi.Utilities;

namespace DiscordBotApi.Models.Applications;

// https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-application-command-data-structure
internal sealed record DiscordApplicationCommandInteractionDataOptionDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "type")]
	int Type,
	[property: JsonPropertyName(name: "value")]
	object? Value,
	[property: JsonPropertyName(name: "options")]
	DiscordApplicationCommandInteractionDataOptionDto[]? Options,
	[property: JsonPropertyName(name: "focused")]
	bool? Focused
)
{
	public DiscordApplicationCommandInteractionDataOption ToModel() =>
		new()
		{
			Name = Name,
			Type = (DiscordApplicationCommandOptionType)Type,
			Value = Value is not null
				? JsonParseUtils.ToObject(type: (DiscordApplicationCommandOptionType)Type, jsonValue: Value)
				: null,
			Options = Options?.Select(selector: o => o.ToModel()).ToArray(),
			Focused = Focused
		};
}