// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordVideoDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

internal record DiscordVideoDto(
	[property: JsonPropertyName(name: "url")]
	string? Url
)
{
	internal DiscordVideoDto(DiscordVideo model) : this(Url: model.Url)
	{
	}
}