// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBulkDeleteMessagesArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

public record DiscordBulkDeleteMessagesArgs
{
	public IReadOnlyCollection<ulong> Messages { get; init; } = ImmutableArray<ulong>.Empty;
}