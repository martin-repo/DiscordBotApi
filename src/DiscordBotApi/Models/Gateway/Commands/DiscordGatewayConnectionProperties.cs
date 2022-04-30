// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayConnectionProperties.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    internal record DiscordGatewayConnectionProperties(string OperatingSystem, string BrowserName, string DeviceName);
}