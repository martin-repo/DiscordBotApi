// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageFlags.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    [Flags]
    public enum DiscordMessageFlags : ulong
    {
        SuppressEmbeds = 1 << 2,
        Ephemeral = 1 << 6
    }
}