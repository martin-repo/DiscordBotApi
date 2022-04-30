// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordHelloDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using System.Text.Json.Serialization;

    internal record DiscordHelloDto([property: JsonPropertyName("heartbeat_interval")] int HeartbeatInterval);
}