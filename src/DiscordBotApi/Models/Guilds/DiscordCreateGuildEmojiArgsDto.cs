// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildEmojiArgsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    using System.Text.Json.Serialization;

    internal record DiscordCreateGuildEmojiArgsDto(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("image")] string Image);
}