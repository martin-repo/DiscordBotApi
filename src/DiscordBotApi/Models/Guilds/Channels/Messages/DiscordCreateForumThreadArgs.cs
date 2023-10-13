// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateForumThreadArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

public record DiscordCreateForumThreadArgs
{
	public IReadOnlyCollection<ulong>? AppliedTags { get; init; }

	public int? AutoArchiveDuration { get; init; }

	public DiscordForumMessageArgs Message { get; init; } = null!;

	public string Name { get; init; } = string.Empty;

	public int? RateLimitPerUser { get; init; }
}