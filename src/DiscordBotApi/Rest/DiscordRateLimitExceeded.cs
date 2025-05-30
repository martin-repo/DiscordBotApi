﻿// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRateLimitExceeded.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Rest;

namespace DiscordBotApi.Rest;

public record DiscordRateLimitExceeded
{
	private readonly CancellationTokenSource _cancellationTokenSource;

	internal DiscordRateLimitExceeded(
		string resource,
		DiscordBucketResponse? bucketResponse,
		DiscordRateLimitScope scope,
		DiscordRateLimitResponse rateLimitResponse,
		CancellationTokenSource cancellationTokenSource
	)
	{
		Resource = resource;
		BucketResponse = bucketResponse;
		Scope = scope;
		RateLimitResponse = rateLimitResponse;
		_cancellationTokenSource = cancellationTokenSource;
	}

	// Null for resources that does not have a (normal) rate limit
	public DiscordBucketResponse? BucketResponse { get; init; }

	public DiscordRateLimitResponse RateLimitResponse { get; init; }

	public string Resource { get; init; }

	public DiscordRateLimitScope Scope { get; init; }

	public void Cancel() => _cancellationTokenSource.Cancel();
}