// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteraction.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Interface.Models.Users;

namespace DiscordBotApi.Interface.Models.Interactions;

public sealed class DiscordInteraction
{
	public required ulong ApplicationId { get; init; }

	public ulong? ChannelId { get; init; }

	public DiscordInteractionData? Data { get; init; }

	public ulong? GuildId { get; init; }

	public required ulong Id { get; init; }

	public DiscordGuildMember? Member { get; init; }

	public DiscordMessage? Message { get; init; }

	public required string Token { get; init; }

	public required DiscordInteractionType Type { get; init; }

	public DiscordUser? User { get; init; }
}