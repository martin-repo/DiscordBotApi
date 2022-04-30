// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordChannelDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    using System.Text.Json.Serialization;

    internal record DiscordChannelDto(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("type")] int Type,
        [property: JsonPropertyName("guild_id")] string? GuildId,
        [property: JsonPropertyName("position")] int? Position,
        [property: JsonPropertyName("permission_overwrites")] DiscordPermissionOverwriteDto[]? PermissionOverwrites,
        [property: JsonPropertyName("name")] string? Name,
        [property: JsonPropertyName("topic")] string? Topic,
        [property: JsonPropertyName("parent_id")] string? ParentId,
        [property: JsonPropertyName("thread_metadata")] DiscordThreadMetadataDto? ThreadMetadata);
}