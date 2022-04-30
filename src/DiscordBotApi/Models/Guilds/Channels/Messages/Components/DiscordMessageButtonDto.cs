// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageButtonDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Guilds.Emojis;

    internal record DiscordMessageButtonDto(
        [property: JsonPropertyName("style")] int Style,
        [property: JsonPropertyName("label")] string? Label,
        [property: JsonPropertyName("emoji")] DiscordEmojiDto? Emoji,
        [property: JsonPropertyName("custom_id")] string? CustomId,
        [property: JsonPropertyName("url")] string? Url,
        [property: JsonPropertyName("disabled")] bool? Disabled) : DiscordMessageComponentDto((int)DiscordMessageComponentType.Button)
    {
        internal DiscordMessageButtonDto(DiscordMessageButton model)
            : this(
                (int)model.Style,
                model.Label,
                model.Emoji != null ? new DiscordEmojiDto(model.Emoji) : null,
                model.CustomId,
                model.Url,
                model.Disabled)
        {
        }
    }
}