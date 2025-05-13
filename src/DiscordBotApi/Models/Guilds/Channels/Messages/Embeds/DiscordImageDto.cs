// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordImageDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

internal sealed record DiscordImageDto(
	[property: JsonPropertyName(name: "url")]
	string Url
)
{
	public static DiscordImageDto FromModel(DiscordImage model) => new(Url: model.Url);

	public DiscordImage ToModel() => new() { Url = Url };
}