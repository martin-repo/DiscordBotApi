// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordFooter.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    public record DiscordFooter()
    {
        internal DiscordFooter(DiscordFooterDto dto)
            : this()
        {
            Text = dto.Text;
            IconUrl = dto.IconUrl;
        }

        public string? IconUrl { get; init; }

        public string Text { get; init; } = "";
    }
}