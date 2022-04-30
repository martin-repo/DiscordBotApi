// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateDmArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    public record DiscordCreateDmArgs
    {
        public ulong RecipientId { get; init; }
    }
}