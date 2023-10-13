// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordFieldDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

internal record DiscordFieldDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "value")]
	string Value,
	[property: JsonPropertyName(name: "inline")]
	bool? Inline
)
{
	internal DiscordFieldDto(DiscordField model) : this(Name: model.Name, Value: model.Value, Inline: model.Inline)
	{
	}
}