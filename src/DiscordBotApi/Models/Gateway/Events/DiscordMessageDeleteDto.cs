// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageDeleteDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/topics/gateway#message-delete-message-delete-event-fields
    internal record DiscordMessageDeleteDto(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("channel_id")] string ChannelId,
        [property: JsonPropertyName("guild_id")] string? GuildId);
}