// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayException.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Gateway;

public class DiscordGatewayException : ApplicationException
{
	public DiscordGatewayException(string message, bool isDisconnected) : base(message: message)
	{
		IsDisconnected = isDisconnected;
	}

	public DiscordGatewayException(string message, bool isDisconnected, Exception innerException) : base(
		message: message,
		innerException: innerException
	)
	{
		IsDisconnected = isDisconnected;
	}

	public bool IsDisconnected { get; }
}