// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRateLimitResponse.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest;

public record DiscordRateLimitResponse
{
	internal DiscordRateLimitResponse(DiscordRateLimitResponseDto dto)
	{
		Message = dto.Message;
		RetryAfter = TimeSpan.FromSeconds(value: dto.RetryAfter);
		Global = dto.Global;
	}

	public bool Global { get; init; }

	public string Message { get; init; }

	public TimeSpan RetryAfter { get; init; }
}