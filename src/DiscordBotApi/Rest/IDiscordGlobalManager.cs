// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordGlobalManager.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Rest
{
    using DiscordBotApi.Models.Rest;

    internal interface IDiscordGlobalManager
    {
        Task GetReservationAsync(DiscordResourceId resourceId, CancellationToken cancellationToken);
    }
}