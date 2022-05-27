// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRateLimit.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest
{
    internal class DiscordRateLimit
    {
        public DiscordRateLimit(string bucket)
        {
            Bucket = bucket;
            UpdateTask = Task.CompletedTask;
        }

        public string Bucket { get; }

        public double DiscordReset { get; set; }

        public int Remaining { get; set; }

        public DateTime Reset { get; set; }

        public DateTime? Retry { get; set; }

        public Task UpdateTask { get; set; }
    }
}