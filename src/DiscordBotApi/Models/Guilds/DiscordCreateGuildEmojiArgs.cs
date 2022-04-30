// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildEmojiArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    public record DiscordCreateGuildEmojiArgs
    {
        public string FilePath { get; init; } = "";

        public string Name { get; init; } = "";
    }
}