// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThumbnailDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

internal sealed record DiscordThumbnailDto(
	[property: JsonPropertyName(name: "url")]
	string Url
)
{
	public static DiscordThumbnailDto FromModel(DiscordThumbnail model) => new(Url: model.Url);

	public DiscordThumbnail ToModel() => new() { Url = Url };
}