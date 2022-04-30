// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildRoleArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    public record DiscordCreateGuildRoleArgs
    {
        public string? Name { get; init; }

        public DiscordPermissions? Permissions { get; init; }
    }
}