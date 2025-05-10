// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayClosedException.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Gateway;

namespace DiscordBotApi.Gateway;

internal class DiscordGatewayClosedException(DiscordGatewayCloseType? closeType, string? closeStatusDescription)
	: ApplicationException
{
	public string? CloseStatusDescription { get; } = closeStatusDescription;

	public DiscordGatewayCloseType? CloseType { get; } = closeType;
}