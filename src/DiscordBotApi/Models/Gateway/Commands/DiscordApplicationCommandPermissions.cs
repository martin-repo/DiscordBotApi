// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandPermissions.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    public record DiscordApplicationCommandPermissions
    {
        internal DiscordApplicationCommandPermissions(DiscordApplicationCommandPermissionsDto dto)
        {
            Id = ulong.Parse(dto.Id);
            Type = (DiscordApplicationCommandPermissionType)dto.Type;
            Permission = dto.Permission;
        }

        public ulong Id { get; init; }

        public bool Permission { get; init; }

        public DiscordApplicationCommandPermissionType Type { get; init; }

        public static ulong GetAllChannelsId(ulong guildId)
        {
            return guildId - 1;
        }

        public static ulong GetEveryoneId(ulong guildId)
        {
            return guildId;
        }
    }
}