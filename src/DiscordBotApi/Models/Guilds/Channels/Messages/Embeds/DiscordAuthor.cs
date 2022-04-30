// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordAuthor.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    public record DiscordAuthor()
    {
        internal DiscordAuthor(DiscordAuthorDto dto)
            : this()
        {
            Name = dto.Name;
            IconUrl = dto.IconUrl;
        }

        public string? IconUrl { get; init; }

        public string Name { get; init; } = "";
    }
}