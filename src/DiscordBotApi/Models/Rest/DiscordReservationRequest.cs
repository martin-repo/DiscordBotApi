// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReservationRequest.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest;

internal sealed record DiscordReservationRequest(
	TaskCompletionSource<IDisposable> ReservationReady,
	CancellationToken CancellationToken
);