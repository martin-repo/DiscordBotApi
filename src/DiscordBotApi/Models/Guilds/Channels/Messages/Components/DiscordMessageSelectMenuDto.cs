// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageSelectMenuDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

// https://discord.com/developers/docs/interactions/message-components#select-menu-object-select-menu-structure
internal sealed record DiscordMessageSelectMenuDto(
	[property: JsonPropertyName(name: "custom_id")]
	string CustomId,
	[property: JsonPropertyName(name: "options")]
	ImmutableArray<DiscordMessageSelectMenuOptionDto>? Options,
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
	public static DiscordMessageSelectMenuDto FromModel(DiscordMessageSelectMenu model) =>
		new(
			CustomId: model.CustomId,
			Options: model.Options?.Select(selector: DiscordMessageSelectMenuOptionDto.FromModel).ToImmutableArray(),
			Placeholder: model.Placeholder,
			MinValues: model.MinValues,
			MaxValues: model.MaxValues,
			Disabled: model.Disabled
		);

	public override DiscordMessageSelectMenu ToModel() =>
		new()
		{
			CustomId = CustomId,
			Options = Options?.Select(selector: o => o.ToModel()).ToImmutableArray(),
			Placeholder = Placeholder,
			MinValues = MinValues,
			MaxValues = MaxValues,
			Disabled = Disabled
		};
}