// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReactionDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Guilds.Emojis;

    internal record DiscordReactionDto(
        [property: JsonPropertyName("count")] int Count,
        [property: JsonPropertyName("me")] bool Me,
        [property: JsonPropertyName("emoji")] DiscordEmojiDto Emoji);
}