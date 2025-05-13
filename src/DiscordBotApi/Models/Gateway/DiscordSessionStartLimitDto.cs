// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordSessionStartLimitDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway;

namespace DiscordBotApi.Models.Gateway;

// https://discord.com/developers/docs/topics/gateway#session-start-limit-object
internal sealed record DiscordSessionStartLimitDto(
	[property: JsonPropertyName(name: "total")]
	int Total,
	[property: JsonPropertyName(name: "remaining")]
	int Remaining,
	[property: JsonPropertyName(name: "reset_after")]
	int ResetAfter,
	[property: JsonPropertyName(name: "max_concurrency")]
	int MaxConcurrency
)
{
	public DiscordSessionStartLimit ToModel() =>
		new()
		{
			Total = Total,
			Remaining = Remaining,
			ResetAfter = ResetAfter,
			MaxConcurrency = MaxConcurrency
		};
}