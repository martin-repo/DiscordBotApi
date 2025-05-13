// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayClosedException.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Gateway;

namespace DiscordBotApi.Gateway;

internal class DiscordGatewayClosedException(DiscordGatewayCloseType? closeType, string? closeStatusDescription)
	: ApplicationException
{
	public string? CloseStatusDescription { get; } = closeStatusDescription;

	public DiscordGatewayCloseType? CloseType { get; } = closeType;
}