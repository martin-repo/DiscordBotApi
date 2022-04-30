// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayConnectionPropertiesDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    using System.Text.Json.Serialization;

    internal record DiscordGatewayConnectionPropertiesDto(
        [property: JsonPropertyName("$os")] string OperatingSystem,
        [property: JsonPropertyName("$browser")] string BrowserName,
        [property: JsonPropertyName("$device")] string DeviceName)
    {
        internal DiscordGatewayConnectionPropertiesDto(DiscordGatewayConnectionProperties model)
            : this(model.OperatingSystem, model.BrowserName, model.DeviceName)
        {
        }
    }
}