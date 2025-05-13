// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordVideoDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

internal sealed record DiscordVideoDto(
	[property: JsonPropertyName(name: "url")]
	string? Url
)
{
	public static DiscordVideoDto FromModel(DiscordVideo model) => new(Url: model.Url);

	public DiscordVideo ToModel() => new() { Url = Url };
}