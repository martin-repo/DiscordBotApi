// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageBase.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public abstract class DiscordMessageBase
{
	public required ulong ChannelId { get; init; }

	public required ulong Id { get; init; }
}