// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberAdd.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using DiscordBotApi.Models.Guilds;

    public record DiscordGuildMemberAdd : DiscordGuildMember
    {
        internal DiscordGuildMemberAdd(DiscordGuildMemberAddDto dto)
            : base(dto)
        {
            GuildId = ulong.Parse(dto.GuildId);
        }

        public ulong GuildId { get; init; }
    }
}