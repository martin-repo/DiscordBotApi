// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThumbnailDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

internal record DiscordThumbnailDto(
	[property: JsonPropertyName(name: "url")]
	string Url
)
{
	internal DiscordThumbnailDto(DiscordThumbnail model) : this(Url: model.Url)
	{
	}
}