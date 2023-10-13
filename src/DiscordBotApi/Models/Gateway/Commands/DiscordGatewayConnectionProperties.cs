// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayConnectionProperties.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands;

internal record DiscordGatewayConnectionProperties(string OperatingSystem, string BrowserName, string DeviceName);