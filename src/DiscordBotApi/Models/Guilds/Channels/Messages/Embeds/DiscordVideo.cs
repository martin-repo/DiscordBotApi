// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordVideo.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    public record DiscordVideo()
    {
        internal DiscordVideo(DiscordVideoDto dto)
            : this()
        {
            Url = dto.Url;
        }

        public string? Url { get; init; }
    }
}