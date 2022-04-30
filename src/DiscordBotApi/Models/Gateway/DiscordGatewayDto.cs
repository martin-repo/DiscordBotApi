// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway
{
    using System.Text.Json.Serialization;

    internal record DiscordGatewayDto([property: JsonPropertyName("url")] string Url);
}