// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordBotClient.Gateway.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Gateway.Commands;

namespace DiscordBotApi.Interface;

public partial interface IDiscordBotClient
{
	Task UpdatePresenceAsync(DiscordPresenceUpdate presenceUpdate, CancellationToken cancellationToken = default);
}