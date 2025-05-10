// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReaction.cs" company="kpop.fan">
//   Copyright (c) 2023 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Emojis;

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public sealed class DiscordReaction
{
	public required int Count { get; init; }

	public required DiscordEmoji Emoji { get; init; }

	public required bool Me { get; init; }
}