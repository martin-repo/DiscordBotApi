// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReference.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    public record DiscordMessageReference
    {
        public ulong? ChannelId { get; init; }

        public bool? FailIfNotExists { get; init; }

        public ulong? GuildId { get; init; }

        public ulong? MessageId { get; init; }
    }
}