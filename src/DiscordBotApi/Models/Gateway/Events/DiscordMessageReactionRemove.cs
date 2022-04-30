// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReactionRemove.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using DiscordBotApi.Models.Guilds.Emojis;

    public record DiscordMessageReactionRemove
    {
        internal DiscordMessageReactionRemove(DiscordMessageReactionRemoveDto dto)
        {
            UserId = ulong.Parse(dto.UserId);
            ChannelId = ulong.Parse(dto.ChannelId);
            MessageId = ulong.Parse(dto.MessageId);
            GuildId = dto.GuildId != null ? ulong.Parse(dto.GuildId) : null;
            Emoji = new DiscordEmoji(dto.Emoji);
        }

        public ulong ChannelId { get; init; }

        public DiscordEmoji Emoji { get; init; }

        public ulong? GuildId { get; init; }

        public ulong MessageId { get; init; }

        public ulong UserId { get; init; }
    }
}