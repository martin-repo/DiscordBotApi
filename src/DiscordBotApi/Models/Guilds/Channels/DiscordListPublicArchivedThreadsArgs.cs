// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordListPublicArchivedThreadsArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    public record DiscordListPublicArchivedThreadsArgs
    {
        public DateTime? Before { get; init; }

        public int? Limit { get; init; }
    }
}