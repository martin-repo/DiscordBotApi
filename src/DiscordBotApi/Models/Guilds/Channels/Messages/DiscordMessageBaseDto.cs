// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageBaseDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/resources/channel#message-object-message-structure
    internal record DiscordMessageBaseDto([property: JsonPropertyName("id")] string Id, [property: JsonPropertyName("channel_id")] string ChannelId);
}