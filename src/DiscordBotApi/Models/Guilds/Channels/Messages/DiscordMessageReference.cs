// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReference.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

public record DiscordMessageReference
{
	public ulong? ChannelId { get; init; }

	public bool? FailIfNotExists { get; init; }

	public ulong? GuildId { get; init; }

	public ulong? MessageId { get; init; }
}