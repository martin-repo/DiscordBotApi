// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayDispatch.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway
{
    using DiscordBotApi.Models.Gateway.Events;

    internal record DiscordGatewayDispatch(DiscordEventType EventType, string EventDataJson);
}