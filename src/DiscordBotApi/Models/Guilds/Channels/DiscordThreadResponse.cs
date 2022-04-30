// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThreadResponse.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    public record DiscordThreadResponse
    {
        internal DiscordThreadResponse(DiscordBotClient botClient, DiscordThreadResponseDto dto)
        {
            Threads = dto.Threads.Select(t => new DiscordChannel(botClient, t)).ToArray();
            HasMore = dto.HasMore;
        }

        public bool HasMore { get; init; }

        public IReadOnlyCollection<DiscordChannel> Threads { get; init; }
    }
}