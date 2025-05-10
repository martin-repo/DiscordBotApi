// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordGlobalManager.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Rest;

namespace DiscordBotApi.Rest;

internal interface IDiscordGlobalManager
{
	Task GetReservationAsync(DiscordResourceId resourceId, CancellationToken cancellationToken);
}