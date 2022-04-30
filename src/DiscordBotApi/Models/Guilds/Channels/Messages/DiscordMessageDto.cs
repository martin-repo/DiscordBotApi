// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
    using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;
    using DiscordBotApi.Models.Users;

    // https://discord.com/developers/docs/resources/channel#message-object-message-structure
    internal record DiscordMessageDto(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("channel_id")] string ChannelId,
        [property: JsonPropertyName("guild_id")] string? GuildId,
        [property: JsonPropertyName("author")] DiscordUserDto Author,
        [property: JsonPropertyName("content")] string Content,
        [property: JsonPropertyName("timestamp")] string Timestamp,
        [property: JsonPropertyName("edited_timestamp")] string? EditedTimestamp,
        [property: JsonPropertyName("attachments")] DiscordMessageAttachmentDto[] Attachments,
        [property: JsonPropertyName("embeds")] DiscordEmbedDto[] Embeds,
        [property: JsonPropertyName("reactions")] DiscordReactionDto[]? Reactions,
        [property: JsonPropertyName("pinned")] bool Pinned,
        [property: JsonPropertyName("thread")] DiscordChannelDto? Thread,
        [property: JsonPropertyName("components")] DiscordMessageActionRowDto[]? Components);
}