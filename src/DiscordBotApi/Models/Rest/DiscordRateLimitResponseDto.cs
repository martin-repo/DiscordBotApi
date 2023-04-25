// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRateLimitResponseDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Rest;

internal record DiscordRateLimitResponseDto(
	[property: JsonPropertyName(name: "message")]
	string Message,
	[property: JsonPropertyName(name: "retry_after")]
	double RetryAfter,
	[property: JsonPropertyName(name: "global")]
	bool Global
);