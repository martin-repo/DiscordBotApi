// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordFooterDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    using System.Text.Json.Serialization;

    internal record DiscordFooterDto([property: JsonPropertyName("text")] string Text, [property: JsonPropertyName("icon_url")] string? IconUrl)
    {
        internal DiscordFooterDto(DiscordFooter model)
            : this(model.Text, model.IconUrl)
        {
        }
    }
}