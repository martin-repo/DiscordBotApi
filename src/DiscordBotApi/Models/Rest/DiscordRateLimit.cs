// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRateLimit.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest;

internal sealed class DiscordRateLimit
{
	public required string Bucket { get; init; }

	public double DiscordReset { get; set; }

	public int Remaining { get; set; }

	public DateTime Reset { get; set; }

	public DateTime? Retry { get; set; }

	public Task UpdateTask { get; set; } = Task.CompletedTask;
}