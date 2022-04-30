// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGetChannelMessagesArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    public record DiscordGetChannelMessagesArgs
    {
        public ulong? After { get; init; }

        public ulong? Around { get; init; }

        public ulong? Before { get; init; }

        public int? Limit { get; init; }
    }
}