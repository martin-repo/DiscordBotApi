// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordActiveThreadResponseDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels;

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/guild#list-active-guild-threads-response-body
internal sealed record DiscordActiveThreadResponseDto(
	[property: JsonPropertyName(name: "threads")]
	DiscordChannelDto[] Threads
)
{
	public DiscordActiveThreadResponse ToModel() =>
		new() { Threads = Threads.Select(selector: t => t.ToModel()).ToArray() };
}