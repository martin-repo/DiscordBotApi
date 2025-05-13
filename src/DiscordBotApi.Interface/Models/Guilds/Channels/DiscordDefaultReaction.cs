// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordDefaultReaction.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#default-reaction-object
public sealed class DiscordDefaultReaction
{
	public ulong? EmojiId { get; init; }

	public string? EmojiName { get; init; }
}