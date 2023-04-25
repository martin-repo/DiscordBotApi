// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayException.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway;

public class DiscordGatewayException : ApplicationException
{
	public DiscordGatewayException(string message, bool isDisconnected) : base(message: message)
	{
		IsDisconnected = isDisconnected;
	}

	public DiscordGatewayException(string message, bool isDisconnected, Exception innerException) : base(
		message: message,
		innerException: innerException)
	{
		IsDisconnected = isDisconnected;
	}

	public bool IsDisconnected { get; }
}