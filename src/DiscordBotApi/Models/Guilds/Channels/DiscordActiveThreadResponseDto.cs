// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordActiveThreadResponseDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/guild#list-active-guild-threads-response-body
internal record DiscordActiveThreadResponseDto(
	[property: JsonPropertyName(name: "threads")]
	DiscordChannelDto[] Threads
);