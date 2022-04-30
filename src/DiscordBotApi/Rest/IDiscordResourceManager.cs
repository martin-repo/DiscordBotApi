// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordResourceManager.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Rest
{
    using DiscordBotApi.Models.Rest;

    internal interface IDiscordResourceManager
    {
        Task<IDisposable> GetReservationAsync(DiscordResourceId resourceId, long requestIndex, CancellationToken cancellationToken);

        DiscordResourceId GetResourceId(string httpMethod, string endpoint);

        void UpdateResource(DiscordResourceId resourceId, DiscordBucketResponse? bucketResponse, DiscordRateLimitResponse? rateLimitResponse);
    }
}