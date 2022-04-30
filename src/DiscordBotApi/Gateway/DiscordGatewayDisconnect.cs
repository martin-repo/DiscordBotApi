// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayDisconnect.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway
{
    using DiscordBotApi.Models.Gateway;

    public record DiscordGatewayDisconnect(DiscordGatewayCloseType? CloseType, string? CloseStatusDescription);
}