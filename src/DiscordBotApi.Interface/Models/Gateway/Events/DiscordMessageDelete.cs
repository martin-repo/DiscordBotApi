// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageDelete.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Gateway.Events;

public sealed class DiscordMessageDelete
{
	public required ulong ChannelId { get; init; }

	public ulong? GuildId { get; init; }

	public required ulong Id { get; init; }
}