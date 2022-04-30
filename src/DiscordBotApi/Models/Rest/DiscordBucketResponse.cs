// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBucketResponse.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest
{
    public record DiscordBucketResponse
    {
        internal DiscordBucketResponse(
            string bucket,
            int limit,
            int remaining,
            double discordReset,
            TimeSpan resetAfter)
        {
            Bucket = bucket;
            Limit = limit;
            Remaining = remaining;
            DiscordReset = discordReset;
            ResetAfter = resetAfter;
        }

        // Rate limit id for (shared) resource
        public string Bucket { get; init; }

        // Max quota for resource
        public int Limit { get; init; }

        // Current quota for resource
        public int Remaining { get; init; }

        // Time until quota is reset
        public TimeSpan ResetAfter { get; init; }

        // Discord reset value -- only used to identify bucket session
        internal double DiscordReset { get; init; }
    }
}