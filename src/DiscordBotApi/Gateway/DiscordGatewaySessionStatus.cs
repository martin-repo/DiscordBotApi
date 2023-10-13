// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewaySessionStatus.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway;

internal enum DiscordGatewaySessionStatus
{
	Connected,
	Disconnecting,
	Disconnected
}