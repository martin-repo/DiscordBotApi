// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReference.cs" company="kpop.fan">
//   Copyright (c) 2023 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public sealed class DiscordMessageReference
{
	

	public ulong? ChannelId { get; init; }

	public bool? FailIfNotExists { get; init; }

	public ulong? GuildId { get; init; }

	public ulong? MessageId { get; init; }
}