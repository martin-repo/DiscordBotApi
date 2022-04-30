// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGetApplicationCommandsArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    public record DiscordGetApplicationCommandsArgs
    {
        public bool? WithLocalizations { get; init; }
    }
}