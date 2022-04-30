// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReactionAddDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Guilds;
    using DiscordBotApi.Models.Guilds.Emojis;

    // https://discord.com/developers/docs/topics/gateway#message-reaction-add-message-reaction-add-event-fields
    internal record DiscordMessageReactionAddDto(
        [property: JsonPropertyName("user_id")] string UserId,
        [property: JsonPropertyName("channel_id")] string ChannelId,
        [property: JsonPropertyName("message_id")] string MessageId,
        [property: JsonPropertyName("guild_id")] string? GuildId,
        [property: JsonPropertyName("member")] DiscordGuildMemberDto? Member,
        [property: JsonPropertyName("emoji")] DiscordEmojiDto Emoji);
}