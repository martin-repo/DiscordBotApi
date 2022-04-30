// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPermissionOverwrite.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    public record DiscordPermissionOverwrite()
    {
        internal DiscordPermissionOverwrite(DiscordPermissionOverwriteDto dto)
            : this()
        {
            Id = ulong.Parse(dto.Id);
            Type = (DiscordPermissionOverwriteType)dto.Type;
            Allow = (DiscordPermissions)ulong.Parse(dto.Allow);
            Deny = (DiscordPermissions)ulong.Parse(dto.Deny);
        }

        public DiscordPermissions Allow { get; init; }

        public DiscordPermissions Deny { get; init; }

        public ulong Id { get; init; }

        public DiscordPermissionOverwriteType Type { get; init; }
    }
}