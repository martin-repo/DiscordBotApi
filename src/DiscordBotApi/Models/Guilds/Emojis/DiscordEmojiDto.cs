// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEmojiDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Emojis
{
    using System.Text.Json.Serialization;

    internal record DiscordEmojiDto(
        [property: JsonPropertyName("id")] string? Id,
        [property: JsonPropertyName("name")] string? Name,
        [property: JsonPropertyName("require_colons")] bool? RequireColons,
        [property: JsonPropertyName("animated")] bool? Animated,
        [property: JsonPropertyName("available")] bool? Available)
    {
        internal DiscordEmojiDto(DiscordEmoji model)
            : this(model.Id?.ToString(), model.Name, model.RequireColons, model.Animated, model.Available)
        {
        }
    }
}