// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildRoleUpdate.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using DiscordBotApi.Models.Guilds;

    public record DiscordGuildRoleUpdate
    {
        internal DiscordGuildRoleUpdate(DiscordBotClient botClient, DiscordGuildRoleUpdateDto dto)
        {
            GuildId = ulong.Parse(dto.GuildId);
            Role = new DiscordRole(botClient, ulong.Parse(dto.GuildId), dto.Role);
        }

        public ulong GuildId { get; init; }

        public DiscordRole Role { get; init; }
    }
}