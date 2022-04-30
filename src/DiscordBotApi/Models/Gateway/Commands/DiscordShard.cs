// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordShard.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    public record DiscordShard
    {
        public int NumShards { get; init; }

        public int ShardId { get; init; }
    }
}