// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageFile.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public sealed class DiscordMessageFile
{
	public required string FilePath { get; init; }

	public required ulong Id { get; init; }
}