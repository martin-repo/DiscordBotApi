// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayBotDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/topics/gateway#get-gateway-bot-json-response
    internal record DiscordGatewayBotDto(
        [property: JsonPropertyName("url")] string Url,
        [property: JsonPropertyName("shards")] int Shards,
        [property: JsonPropertyName("session_start_limit")] DiscordSessionStartLimitDto SessionStartLimit);
}