// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReservationRequest.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Rest
{
    internal record DiscordReservationRequest(TaskCompletionSource<IDisposable> ReservationReady, CancellationToken CancellationToken);
}