// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordAuthorDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

internal record DiscordAuthorDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "icon_url")]
	string? IconUrl
)
{
	internal DiscordAuthorDto(DiscordAuthor model) : this(Name: model.Name, IconUrl: model.IconUrl)
	{
	}
}