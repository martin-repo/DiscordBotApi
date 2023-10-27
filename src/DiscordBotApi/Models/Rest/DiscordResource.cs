// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordResource.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest;

internal record DiscordResource
{
	public DiscordResource(DiscordResourceId id, DiscordRateLimit rateLimit)
	{
		Id = id;
		RateLimit = rateLimit;
		ReservationRequests = new SortedDictionary<long, DiscordReservationRequest>();
	}

	public DiscordResourceId Id { get; set; }

	public DiscordRateLimit RateLimit { get; set; }

	public SortedDictionary<long, DiscordReservationRequest> ReservationRequests { get; }
}