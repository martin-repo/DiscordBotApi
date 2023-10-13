// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageFile.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

public record DiscordMessageFile
{
	public string FilePath { get; init; } = "";

	public ulong Id { get; init; }
}