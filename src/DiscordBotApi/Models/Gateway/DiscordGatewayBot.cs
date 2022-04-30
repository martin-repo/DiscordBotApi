// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayBot.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway
{
    public record DiscordGatewayBot
    {
        internal DiscordGatewayBot(DiscordGatewayBotDto dto)
        {
            Url = dto.Url;
            Shards = dto.Shards;
            SessionStartLimit = new DiscordSessionStartLimit(dto.SessionStartLimit);
        }

        public DiscordSessionStartLimit SessionStartLimit { get; init; }

        public int Shards { get; init; }

        public string Url { get; init; }
    }
}