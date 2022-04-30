// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBulkDeleteMessagesArgsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using System.Text.Json.Serialization;

    internal record DiscordBulkDeleteMessagesArgsDto([property: JsonPropertyName("messages")] string[] Messages)
    {
        internal DiscordBulkDeleteMessagesArgsDto(DiscordBulkDeleteMessagesArgs model)
            : this(model.Messages.Select(m => m.ToString()).ToArray())
        {
        }
    }
}