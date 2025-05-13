// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBucketResponse.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Rest;

public sealed class DiscordBucketResponse
{
	// Rate limit id for (shared) resource
	public required string Bucket { get; init; }

	// Discord reset value -- only used to identify bucket session
	public required double DiscordReset { get; init; }

	// Max quota for resource
	public required int Limit { get; init; }

	// Current quota for resource
	public required int Remaining { get; init; }

	// Time until quota is reset
	public required TimeSpan ResetAfter { get; init; }
}