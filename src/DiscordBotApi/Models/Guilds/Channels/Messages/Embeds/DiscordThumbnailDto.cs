// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThumbnailDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    using System.Text.Json.Serialization;

    internal record DiscordThumbnailDto([property: JsonPropertyName("url")] string Url)
    {
        internal DiscordThumbnailDto(DiscordThumbnail model)
            : this(model.Url)
        {
        }
    }
}