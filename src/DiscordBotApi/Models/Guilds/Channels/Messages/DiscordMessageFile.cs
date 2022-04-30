// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageFile.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    public record DiscordMessageFile
    {
        public ulong Id { get; init; }

        public string FilePath { get; init; } = "";
    }
}