// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordSessionStartLimit.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway
{
    public record DiscordSessionStartLimit
    {
        internal DiscordSessionStartLimit(DiscordSessionStartLimitDto dto)
        {
            Total = dto.Total;
            Remaining = dto.Remaining;
            ResetAfter = dto.ResetAfter;
            MaxConcurrency = dto.MaxConcurrency;
        }

        public int MaxConcurrency { get; init; }

        public int Remaining { get; init; }

        public int ResetAfter { get; init; }

        public int Total { get; init; }
    }
}