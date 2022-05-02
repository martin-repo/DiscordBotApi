// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordUpdatedMessageDto.cs" company="kpop.fan">
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
    // This class is a special-case for the "MESSAGE_UPDATE" gateway event where all fields can be null.
    // https://discord.com/developers/docs/topics/gateway#message-update
    internal record DiscordUpdatedMessageDto(
        string Id,
        string ChannelId,
        [property: JsonPropertyName("guild_id")] string? GuildId,
        [property: JsonPropertyName("author")] DiscordUserDto? Author,
        [property: JsonPropertyName("content")] string? Content,
        [property: JsonPropertyName("timestamp")] string? Timestamp,
        [property: JsonPropertyName("edited_timestamp")] string? EditedTimestamp,
        [property: JsonPropertyName("attachments")] DiscordMessageAttachmentDto[]? Attachments,
        [property: JsonPropertyName("embeds")] DiscordEmbedDto[]? Embeds,
        [property: JsonPropertyName("reactions")] DiscordReactionDto[]? Reactions,
        [property: JsonPropertyName("pinned")] bool? Pinned,
        [property: JsonPropertyName("thread")] DiscordChannelDto? Thread,
        [property: JsonPropertyName("components")] DiscordMessageActionRowDto[]? Components) :
        DiscordMessageBaseDto(Id, ChannelId);
}