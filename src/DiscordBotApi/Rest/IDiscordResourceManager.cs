// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordResourceManager.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Rest;
using DiscordBotApi.Models.Rest;

namespace DiscordBotApi.Rest;

internal interface IDiscordResourceManager
{
	Task<IDisposable> GetReservationAsync(
		DiscordResourceId resourceId,
		long requestIndex,
		CancellationToken cancellationToken
	);

	DiscordResourceId GetResourceId(string httpMethod, string endpoint);

	void UpdateResource(
		DiscordResourceId resourceId,
		DiscordBucketResponse? bucketResponse,
		DiscordRateLimitResponse? rateLimitResponse
	);
}