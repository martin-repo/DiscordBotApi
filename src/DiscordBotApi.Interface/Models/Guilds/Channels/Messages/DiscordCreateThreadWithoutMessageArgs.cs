// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateThreadWithoutMessageArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public sealed class DiscordCreateThreadWithoutMessageArgs
{
	public int? AutoArchiveDuration { get; init; }

	public bool? Invitable { get; init; }

	public required string Name { get; init; }

	public int? RateLimitPerUser { get; init; }

	public DiscordChannelType? Type { get; init; }
}