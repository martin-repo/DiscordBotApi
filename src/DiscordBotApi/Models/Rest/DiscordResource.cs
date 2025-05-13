// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordResource.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest;

internal sealed class DiscordResource
{
	public required DiscordResourceId Id { get; init; }

	public required DiscordRateLimit RateLimit { get; set; }

	public required SortedDictionary<long, DiscordReservationRequest> ReservationRequests { get; init; }
}