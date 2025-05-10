// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordAuthorDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

internal sealed record DiscordAuthorDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "url")]
	string? Url,
	[property: JsonPropertyName(name: "icon_url")]
	string? IconUrl
)
{
	public static DiscordAuthorDto FromModel(DiscordAuthor model) =>
		new(Name: model.Name, Url: model.Url, IconUrl: model.IconUrl);

	public DiscordAuthor ToModel() =>
		new()
		{
			Name = Name,
			Url = Url,
			IconUrl = IconUrl
		};
}