// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRateLimitResponseDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Rest;

namespace DiscordBotApi.Models.Rest;

internal sealed record DiscordRateLimitResponseDto(
	[property: JsonPropertyName(name: "message")]
	string Message,
	[property: JsonPropertyName(name: "retry_after")]
	double RetryAfter,
	[property: JsonPropertyName(name: "global")]
	bool Global
)
{
	public DiscordRateLimitResponse ToModel() =>
		new()
		{
			Message = Message,
			RetryAfter = TimeSpan.FromSeconds(value: RetryAfter),
			Global = Global
		};
}