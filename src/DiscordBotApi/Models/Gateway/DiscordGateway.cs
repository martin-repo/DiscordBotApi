// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGateway.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway
{
    public record DiscordGateway
    {
        internal DiscordGateway(DiscordGatewayDto dto)
        {
            Url = dto.Url;
        }

        public string Url { get; init; }
    }
}