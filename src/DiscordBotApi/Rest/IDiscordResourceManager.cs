// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordResourceManager.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Rest;

namespace DiscordBotApi.Rest;

internal interface IDiscordResourceManager
{
	Task<IDisposable> GetReservationAsync(DiscordResourceId resourceId, long requestIndex, CancellationToken cancellationToken);

	DiscordResourceId GetResourceId(string httpMethod, string endpoint);

	void UpdateResource(
		DiscordResourceId resourceId,
		DiscordBucketResponse? bucketResponse,
		DiscordRateLimitResponse? rateLimitResponse
	);
}