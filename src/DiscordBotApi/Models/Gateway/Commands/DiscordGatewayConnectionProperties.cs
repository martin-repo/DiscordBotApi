// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayConnectionProperties.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands;

internal sealed record DiscordGatewayConnectionProperties(
	string OperatingSystem,
	string BrowserName,
	string DeviceName
);