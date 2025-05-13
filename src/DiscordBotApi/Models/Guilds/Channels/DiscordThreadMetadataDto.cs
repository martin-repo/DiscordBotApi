// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThreadMetadataDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels;

namespace DiscordBotApi.Models.Guilds.Channels;

internal sealed record DiscordThreadMetadataDto(
	[property: JsonPropertyName(name: "archived")]
	bool Archived,
	[property: JsonPropertyName(name: "archive_timestamp")]
	string ArchiveTimestamp
)
{
	public DiscordThreadMetadata ToModel() =>
		new()
		{
			Archived = Archived,
			ArchiveTimestamp = DateTime.Parse(s: ArchiveTimestamp)
		};
}