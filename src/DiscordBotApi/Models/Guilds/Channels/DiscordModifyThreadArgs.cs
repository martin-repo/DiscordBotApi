// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyThreadArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    public record DiscordModifyThreadArgs
    {
        public bool? Archived { get; init; }
    }
}