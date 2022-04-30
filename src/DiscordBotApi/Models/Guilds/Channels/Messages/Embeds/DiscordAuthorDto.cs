// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordAuthorDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    using System.Text.Json.Serialization;

    internal record DiscordAuthorDto([property: JsonPropertyName("name")] string Name, [property: JsonPropertyName("icon_url")] string? IconUrl)
    {
        internal DiscordAuthorDto(DiscordAuthor model)
            : this(model.Name, model.IconUrl)
        {
        }
    }
}