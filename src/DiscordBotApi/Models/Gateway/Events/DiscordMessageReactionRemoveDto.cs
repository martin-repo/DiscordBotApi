// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReactionRemoveDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Guilds.Emojis;

    // https://discord.com/developers/docs/topics/gateway#message-reaction-remove-message-reaction-remove-event-fields
    internal record DiscordMessageReactionRemoveDto(
        [property: JsonPropertyName("user_id")] string UserId,
        [property: JsonPropertyName("channel_id")] string ChannelId,
        [property: JsonPropertyName("message_id")] string MessageId,
        [property: JsonPropertyName("guild_id")] string? GuildId,
        [property: JsonPropertyName("emoji")] DiscordEmojiDto Emoji);
}