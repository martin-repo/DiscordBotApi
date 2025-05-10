// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordErrorResponse.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Rest;

public sealed class DiscordErrorResponse
{
	public required DiscordJsonErrorCode Code { get; init; }

	public string? JsonKey { get; init; }

	public required string Message { get; init; }
}