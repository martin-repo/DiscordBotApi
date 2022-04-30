// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordVideoDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    using System.Text.Json.Serialization;

    public record DiscordVideoDto([property: JsonPropertyName("url")] string? Url)
    {
        internal DiscordVideoDto(DiscordVideo model)
            : this(model.Url)
        {
        }
    }
}