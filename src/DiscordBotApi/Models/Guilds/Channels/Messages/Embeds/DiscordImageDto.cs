// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordImageDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    using System.Text.Json.Serialization;

    internal record DiscordImageDto([property: JsonPropertyName("url")] string Url)
    {
        internal DiscordImageDto(DiscordImage model)
            : this(model.Url)
        {
        }
    }
}