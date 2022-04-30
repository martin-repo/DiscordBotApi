// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordImage.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    public record DiscordImage()
    {
        internal DiscordImage(DiscordImageDto dto)
            : this()
        {
            Url = dto.Url;
        }

        public string Url { get; init; } = "";
    }
}