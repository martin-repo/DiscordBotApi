// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThreadMetadata.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    public record DiscordThreadMetadata
    {
        internal DiscordThreadMetadata(DiscordThreadMetadataDto dto)
        {
            Archived = dto.Archived;
            ArchiveTimestamp = DateTime.Parse(dto.ArchiveTimestamp);
        }

        public bool Archived { get; init; }

        public DateTime ArchiveTimestamp { get; init; }
    }
}