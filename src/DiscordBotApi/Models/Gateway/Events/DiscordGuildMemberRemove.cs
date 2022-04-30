// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberRemove.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using DiscordBotApi.Models.Users;

    public record DiscordGuildMemberRemove
    {
        internal DiscordGuildMemberRemove(DiscordGuildMemberRemoveDto dto)
        {
            GuildId = ulong.Parse(dto.GuildId);
            User = new DiscordUser(dto.User);
        }

        public ulong GuildId { get; init; }

        public DiscordUser User { get; init; }
    }
}