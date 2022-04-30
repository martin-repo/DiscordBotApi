// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMember.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    using DiscordBotApi.Models.Users;

    public record DiscordGuildMember
    {
        internal DiscordGuildMember(DiscordGuildMemberDto dto)
        {
            User = dto.User != null ? new DiscordUser(dto.User) : null;
            Nick = dto.Nick;
            Roles = dto.Roles.Select(ulong.Parse).ToArray();
        }

        public string? Nick { get; init; }

        public IReadOnlyCollection<ulong> Roles { get; init; }

        public DiscordUser? User { get; init; }
    }
}