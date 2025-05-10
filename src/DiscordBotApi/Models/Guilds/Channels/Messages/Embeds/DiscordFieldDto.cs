// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordFieldDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

internal sealed record DiscordFieldDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "value")]
	string Value,
	[property: JsonPropertyName(name: "inline")]
	bool? Inline
)
{
	public static DiscordFieldDto FromModel(DiscordField model) =>
		new(Name: model.Name, Value: model.Value, Inline: model.Inline);

	public DiscordField ToModel() =>
		new()
		{
			Name = Name,
			Value = Value,
			Inline = Inline
		};
}