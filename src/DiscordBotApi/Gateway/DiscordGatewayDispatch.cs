// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayDispatch.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Gateway.Events;

namespace DiscordBotApi.Gateway;

internal record DiscordGatewayDispatch(DiscordEventType EventType, string EventDataJson);