// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThreadResponseDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels;

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#list-public-archived-threads-response-body
internal sealed record DiscordThreadResponseDto(
	[property: JsonPropertyName(name: "threads")]
	DiscordChannelDto[] Threads,
	[property: JsonPropertyName(name: "has_more")]
	bool HasMore
)
{
	public DiscordThreadResponse ToModel() =>
		new()
		{
			Threads = Threads.Select(selector: t => t.ToModel()).ToArray(),
			HasMore = HasMore
		};
}