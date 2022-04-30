// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyGuildRoleArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    public record DiscordModifyGuildRoleArgs
    {
        public string? Name { get; init; }

        public DiscordPermissions? Permissions { get; init; }
    }
}