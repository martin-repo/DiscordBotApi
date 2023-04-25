// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReaction.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

public record DiscordReaction
{
	internal DiscordReaction(DiscordReactionDto dto)
	{
		Count = dto.Count;
		Me = dto.Me;
		Emoji = new DiscordEmoji(dto: dto.Emoji);
	}

	public int Count { get; init; }

	public DiscordEmoji Emoji { get; init; }

	public bool Me { get; init; }
}