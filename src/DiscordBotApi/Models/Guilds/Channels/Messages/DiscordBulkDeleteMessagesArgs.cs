// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBulkDeleteMessagesArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

public record DiscordBulkDeleteMessagesArgs
{
	public IReadOnlyCollection<ulong> Messages { get; set; } = Array.Empty<ulong>();
}