// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThreadMetadataDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    using System.Text.Json.Serialization;

    internal record DiscordThreadMetadataDto(
        [property: JsonPropertyName("archived")] bool Archived,
        [property: JsonPropertyName("archive_timestamp")] string ArchiveTimestamp);
}