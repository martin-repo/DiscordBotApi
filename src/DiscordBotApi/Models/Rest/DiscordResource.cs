// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordResource.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest
{
    internal class DiscordResource
    {
        public DiscordResource(DiscordResourceId id, DiscordRateLimit rateLimit)
        {
            Id = id;
            RateLimit = rateLimit;
            ReservationRequests = new();
        }

        public DiscordResourceId Id { get; set; }

        public SortedDictionary<long, DiscordReservationRequest> ReservationRequests { get; }

        public DiscordRateLimit RateLimit { get; set; }
    }
}