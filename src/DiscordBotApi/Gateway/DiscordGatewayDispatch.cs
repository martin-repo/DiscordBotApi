// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayDispatch.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Gateway.Events;

namespace DiscordBotApi.Gateway;

internal sealed record DiscordGatewayDispatch(DiscordEventType EventType, string EventDataJson);