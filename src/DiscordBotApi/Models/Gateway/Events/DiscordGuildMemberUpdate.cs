// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberUpdate.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using DiscordBotApi.Models.Users;

    public record DiscordGuildMemberUpdate
    {
        internal DiscordGuildMemberUpdate(DiscordGuildMemberUpdateDto dto)
        {
            GuildId = ulong.Parse(dto.GuildId);
            Roles = dto.Roles.Select(ulong.Parse).ToArray();
            User = new DiscordUser(dto.User);
            Nick = dto.Nick;
        }

        public ulong GuildId { get; init; }

        public string? Nick { get; init; }

        public IReadOnlyCollection<ulong> Roles { get; init; }

        public DiscordUser User { get; init; }
    }
}