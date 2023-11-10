// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateThreadWithoutMessageArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

public record DiscordCreateThreadWithoutMessageArgs
{
	public int? AutoArchiveDuration { get; init; }

	public bool? Invitable { get; init; }

	public string Name { get; init; } = string.Empty;

	public int? RateLimitPerUser { get; init; }

	public DiscordChannelType? Type { get; init; }
}