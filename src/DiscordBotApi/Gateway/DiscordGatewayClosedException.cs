// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayClosedException.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Gateway;

namespace DiscordBotApi.Gateway;

internal class DiscordGatewayClosedException : ApplicationException
{
	public DiscordGatewayClosedException(DiscordGatewayCloseType? closeType, string? closeStatusDescription)
	{
		CloseType = closeType;
		CloseStatusDescription = closeStatusDescription;
	}

	public string? CloseStatusDescription { get; }

	public DiscordGatewayCloseType? CloseType { get; }
}