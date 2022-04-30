// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBulkDeleteMessagesArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    public record DiscordBulkDeleteMessagesArgs
    {
        public IReadOnlyCollection<ulong> Messages { get; set; } = Array.Empty<ulong>();
    }
}