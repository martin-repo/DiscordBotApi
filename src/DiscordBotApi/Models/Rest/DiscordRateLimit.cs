// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRateLimit.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest;

internal record DiscordRateLimit(string Bucket)
{
	public double DiscordReset { get; set; }

	public int Remaining { get; set; }

	public DateTime Reset { get; set; }

	public DateTime? Retry { get; set; }

	public Task UpdateTask { get; set; } = Task.CompletedTask;
}