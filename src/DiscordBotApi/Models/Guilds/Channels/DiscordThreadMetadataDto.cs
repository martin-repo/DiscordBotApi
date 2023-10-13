// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThreadMetadataDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels;

internal record DiscordThreadMetadataDto(
	[property: JsonPropertyName(name: "archived")]
	bool Archived,
	[property: JsonPropertyName(name: "archive_timestamp")]
	string ArchiveTimestamp
);