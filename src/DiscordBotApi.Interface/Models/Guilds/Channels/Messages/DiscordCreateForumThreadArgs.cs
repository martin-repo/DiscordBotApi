// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateForumThreadArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public sealed class DiscordCreateForumThreadArgs
{
	public IReadOnlyCollection<ulong>? AppliedTags { get; init; }

	public int? AutoArchiveDuration { get; init; }

	public required DiscordForumMessageArgs Message { get; init; }

	public required string Name { get; init; }

	public int? RateLimitPerUser { get; init; }
}