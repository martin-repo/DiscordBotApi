// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReservationRequest.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest;

internal record DiscordReservationRequest(
	TaskCompletionSource<IDisposable> ReservationReady,
	CancellationToken CancellationToken
);