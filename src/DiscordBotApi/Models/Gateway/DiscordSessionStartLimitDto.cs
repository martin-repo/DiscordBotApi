// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordSessionStartLimitDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway;

// https://discord.com/developers/docs/topics/gateway#session-start-limit-object
internal record DiscordSessionStartLimitDto(
	[property: JsonPropertyName(name: "total")]
	int Total,
	[property: JsonPropertyName(name: "remaining")]
	int Remaining,
	[property: JsonPropertyName(name: "reset_after")]
	int ResetAfter,
	[property: JsonPropertyName(name: "max_concurrency")]
	int MaxConcurrency
);