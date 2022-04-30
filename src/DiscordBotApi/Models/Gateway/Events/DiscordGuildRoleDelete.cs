// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildRoleDelete.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    public record DiscordGuildRoleDelete
    {
        internal DiscordGuildRoleDelete(DiscordGuildRoleDeleteDto dto)
        {
            GuildId = ulong.Parse(dto.GuildId);
            RoleId = ulong.Parse(dto.RoleId);
        }

        public ulong GuildId { get; init; }

        public ulong RoleId { get; init; }
    }
}