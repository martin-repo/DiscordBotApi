// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThreadResponseDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#list-public-archived-threads-response-body
internal record DiscordThreadResponseDto(
	[property: JsonPropertyName(name: "threads")]
	DiscordChannelDto[] Threads,
	[property: JsonPropertyName(name: "has_more")]
	bool HasMore
);