// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThumbnail.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    public record DiscordThumbnail()
    {
        internal DiscordThumbnail(DiscordThumbnailDto dto)
            : this()
        {
            Url = dto.Url;
        }

        public string Url { get; init; } = "";
    }
}