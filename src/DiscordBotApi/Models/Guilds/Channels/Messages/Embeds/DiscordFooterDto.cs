// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordFooterDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

internal record DiscordFooterDto(
	[property: JsonPropertyName(name: "text")]
	string Text,
	[property: JsonPropertyName(name: "icon_url")]
	string? IconUrl
)
{
	internal DiscordFooterDto(DiscordFooter model) : this(Text: model.Text, IconUrl: model.IconUrl)
	{
	}
}