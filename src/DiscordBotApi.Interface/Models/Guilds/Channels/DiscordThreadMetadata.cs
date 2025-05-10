// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThreadMetadata.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

public sealed class DiscordThreadMetadata
{
	public required bool Archived { get; init; }

	public required DateTime ArchiveTimestamp { get; init; }
}