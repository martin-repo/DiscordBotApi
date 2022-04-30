// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageDelete.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    public record DiscordMessageDelete
    {
        internal DiscordMessageDelete(DiscordMessageDeleteDto dto)
        {
            Id = ulong.Parse(dto.Id);
            ChannelId = ulong.Parse(dto.ChannelId);
            GuildId = dto.GuildId != null ? ulong.Parse(dto.GuildId) : null;
        }

        public ulong ChannelId { get; init; }

        public ulong? GuildId { get; init; }

        public ulong Id { get; init; }
    }
}