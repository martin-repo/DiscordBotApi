// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReferenceDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/resources/channel#message-reference-object
    internal record DiscordMessageReferenceDto(
        [property: JsonPropertyName("message_id")] string? MessageId,
        [property: JsonPropertyName("channel_id")] string? ChannelId,
        [property: JsonPropertyName("guild_id")] string? GuildId,
        [property: JsonPropertyName("fail_if_not_exists")] bool? FailIfNotExists)
    {
        internal DiscordMessageReferenceDto(DiscordMessageReference model)
            : this(model.MessageId?.ToString(), model.ChannelId?.ToString(), model.GuildId?.ToString(), model.FailIfNotExists)
        {
        }
    }
}