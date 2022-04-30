// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildApplicationCommandPermissions.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    public record DiscordGuildApplicationCommandPermissions
    {
        internal DiscordGuildApplicationCommandPermissions(DiscordGuildApplicationCommandPermissionsDto dto)
        {
            Id = ulong.Parse(dto.Id);
            ApplicationId = ulong.Parse(dto.ApplicationId);
            GuildId = ulong.Parse(dto.GuildId);
            Permissions = dto.Permissions.Select(p => new DiscordApplicationCommandPermissions(p)).ToArray();
        }

        public ulong ApplicationId { get; init; }

        public ulong GuildId { get; init; }

        public ulong Id { get; init; }

        public IReadOnlyCollection<DiscordApplicationCommandPermissions> Permissions { get; init; }
    }
}