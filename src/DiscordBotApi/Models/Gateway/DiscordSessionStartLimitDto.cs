// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/topics/gateway#session-start-limit-object
    internal record DiscordSessionStartLimitDto(
        [property: JsonPropertyName("total")] int Total,
        [property: JsonPropertyName("remaining")] int Remaining,
        [property: JsonPropertyName("reset_after")] int ResetAfter,
        [property: JsonPropertyName("max_concurrency")] int MaxConcurrency);
}