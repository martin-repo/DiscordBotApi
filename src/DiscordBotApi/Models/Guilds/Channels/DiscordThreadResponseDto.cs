// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThreadResponseDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/resources/channel#list-public-archived-threads-response-body
    internal record DiscordThreadResponseDto(
        [property: JsonPropertyName("threads")] DiscordChannelDto[] Threads,
        [property: JsonPropertyName("has_more")] bool HasMore);
}