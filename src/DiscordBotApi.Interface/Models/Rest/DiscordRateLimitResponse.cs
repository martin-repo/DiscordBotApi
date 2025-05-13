// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRateLimitResponse.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Rest;

public sealed class DiscordRateLimitResponse
{
	public required bool Global { get; init; }

	public required string Message { get; init; }

	public required TimeSpan RetryAfter { get; init; }
}