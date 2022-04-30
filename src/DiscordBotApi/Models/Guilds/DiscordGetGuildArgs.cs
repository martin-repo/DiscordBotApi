// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGetGuildArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    public record DiscordGetGuildArgs
    {
        public bool? WithCounts { get; init; }
    }
}