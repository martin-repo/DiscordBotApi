// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordStartThreadWithMessageArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    public record DiscordStartThreadWithMessageArgs
    {
        public AutoArchiveDuration? AutoArchiveDuration { get; init; }

        public string Name { get; init; } = "";
    }
}