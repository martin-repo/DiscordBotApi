// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageSelectMenuDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

// https://discord.com/developers/docs/interactions/message-components#select-menu-object-select-menu-structure
internal record DiscordMessageSelectMenuDto(
	[property: JsonPropertyName(name: "custom_id")]
	string CustomId,
	[property: JsonPropertyName(name: "options")]
	DiscordMessageSelectMenuOptionDto[]? Options,
	[property: JsonPropertyName(name: "placeholder")]
	string? Placeholder,
	[property: JsonPropertyName(name: "min_values")]
	int? MinValues,
	[property: JsonPropertyName(name: "max_values")]
	int? MaxValues,
	[property: JsonPropertyName(name: "disabled")]
	bool? Disabled
) : DiscordMessageComponentDto(Type: (int)DiscordMessageComponentType.SelectMenu)
{
	internal DiscordMessageSelectMenuDto(DiscordMessageSelectMenu model) : this(
		CustomId: model.CustomId,
		Options: model.Options?.Select(selector: o => new DiscordMessageSelectMenuOptionDto(model: o))
			.ToArray(),
		Placeholder: model.Placeholder,
		MinValues: model.MinValues,
		MaxValues: model.MaxValues,
		Disabled: model.Disabled)
	{
	}
}