// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInstallParams.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    using DiscordBotApi.Models.Guilds;

    public record DiscordInstallParams
    {
        internal DiscordInstallParams(DiscordInstallParamsDto dto)
        {
            Scopes = dto.Scopes;
            Permissions = (DiscordPermissions)ulong.Parse(dto.Permissions);
        }

        public DiscordPermissions Permissions { get; init; }

        public IReadOnlyCollection<string> Scopes { get; init; }
    }
}