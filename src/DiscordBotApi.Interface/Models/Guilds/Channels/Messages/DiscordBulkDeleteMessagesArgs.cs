// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBulkDeleteMessagesArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public sealed class DiscordBulkDeleteMessagesArgs
{
	public required IReadOnlyCollection<ulong> Messages { get; init; }
}