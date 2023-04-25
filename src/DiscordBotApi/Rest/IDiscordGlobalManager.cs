// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordGlobalManager.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Rest;

namespace DiscordBotApi.Rest;

internal interface IDiscordGlobalManager
{
	Task GetReservationAsync(DiscordResourceId resourceId, CancellationToken cancellationToken);
}