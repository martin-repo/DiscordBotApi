// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReaction.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using DiscordBotApi.Models.Guilds.Emojis;

    public record DiscordReaction
    {
        internal DiscordReaction(DiscordReactionDto dto)
        {
            Count = dto.Count;
            Me = dto.Me;
            Emoji = new DiscordEmoji(dto.Emoji);
        }

        public int Count { get; init; }

        public DiscordEmoji Emoji { get; init; }

        public bool Me { get; init; }
    }
}