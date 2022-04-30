// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRateLimitResponse.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest
{
    public record DiscordRateLimitResponse
    {
        internal DiscordRateLimitResponse(DiscordRateLimitResponseDto dto)
        {
            Message = dto.Message;
            RetryAfter = TimeSpan.FromSeconds(dto.RetryAfter);
            Global = dto.Global;
        }

        public bool Global { get; init; }

        public string Message { get; init; }

        public TimeSpan RetryAfter { get; init; }
    }
}