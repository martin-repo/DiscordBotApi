// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReactionRemove.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Emojis;

namespace DiscordBotApi.Interface.Models.Gateway.Events;

public sealed class DiscordMessageReactionRemove
{
	public required ulong ChannelId { get; init; }

	public required DiscordEmoji Emoji { get; init; }

	public ulong? GuildId { get; init; }

	public required ulong MessageId { get; init; }

	public required ulong UserId { get; init; }
}