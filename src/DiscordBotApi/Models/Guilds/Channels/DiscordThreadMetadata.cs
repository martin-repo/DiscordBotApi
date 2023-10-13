// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThreadMetadata.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels;

public record DiscordThreadMetadata
{
	internal DiscordThreadMetadata(DiscordThreadMetadataDto dto)
	{
		Archived = dto.Archived;
		ArchiveTimestamp = DateTime.Parse(s: dto.ArchiveTimestamp);
	}

	public bool Archived { get; init; }

	public DateTime ArchiveTimestamp { get; init; }
}