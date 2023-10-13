// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordImageDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

internal record DiscordImageDto(
	[property: JsonPropertyName(name: "url")]
	string Url
)
{
	internal DiscordImageDto(DiscordImage model) : this(Url: model.Url)
	{
	}
}