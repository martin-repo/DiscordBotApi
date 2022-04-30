// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRateLimitResponseDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest
{
    using System.Text.Json.Serialization;

    internal record DiscordRateLimitResponseDto(
        [property: JsonPropertyName("message")] string Message,
        [property: JsonPropertyName("retry_after")] double RetryAfter,
        [property: JsonPropertyName("global")] bool Global);
}