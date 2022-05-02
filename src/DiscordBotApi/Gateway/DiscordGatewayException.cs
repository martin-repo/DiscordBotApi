// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayException.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway
{
    public class DiscordGatewayException : ApplicationException
    {
        public DiscordGatewayException(string message, bool isDisconnected)
            : base(message)
        {
            IsDisconnected = isDisconnected;
        }

        public DiscordGatewayException(string message, bool isDisconnected, Exception innerException)
            : base(message, innerException)
        {
            IsDisconnected = isDisconnected;
        }

        public bool IsDisconnected { get; }
    }
}