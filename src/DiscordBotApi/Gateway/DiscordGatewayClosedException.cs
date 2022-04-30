// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayClosedException.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway
{
    using DiscordBotApi.Models.Gateway;

    internal class DiscordGatewayClosedException : ApplicationException
    {
        public DiscordGatewayClosedException(DiscordGatewayCloseType? closeType, string? closeStatusDescription)
        {
            CloseType = closeType;
            CloseStatusDescription = closeStatusDescription;
        }

        public string? CloseStatusDescription { get; }

        public DiscordGatewayCloseType? CloseType { get; }
    }
}