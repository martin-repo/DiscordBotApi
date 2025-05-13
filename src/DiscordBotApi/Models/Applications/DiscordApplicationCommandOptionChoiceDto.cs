// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOptionChoiceDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Applications;
using DiscordBotApi.Utilities;

namespace DiscordBotApi.Models.Applications;

// https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-choice-structure
internal sealed record DiscordApplicationCommandOptionChoiceDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "value")]
	object Value
)
{
	public static DiscordApplicationCommandOptionChoiceDto FromModel(DiscordApplicationCommandOptionChoice model) =>
		new(Name: model.Name, Value: model.Value);

	public DiscordApplicationCommandOptionChoice ToModel(DiscordApplicationCommandOptionType type) =>
		new()
		{
			Name = Name,
			Value = JsonParseUtils.ToObject(type: type, jsonValue: Value)
		};
}