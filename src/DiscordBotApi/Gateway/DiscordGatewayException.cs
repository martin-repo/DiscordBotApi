// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayException.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway
{
    public class DiscordGatewayException : ApplicationException
    {
        public DiscordGatewayException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}