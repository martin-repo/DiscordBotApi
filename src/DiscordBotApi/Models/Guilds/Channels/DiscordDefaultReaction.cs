// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordDefaultReaction.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#default-reaction-object
public record DiscordDefaultReaction()
{
	internal DiscordDefaultReaction(DiscordDefaultReactionDto dto) : this()
	{
		EmojiId = dto.EmojiId;
		EmojiName = dto.EmojiName;
	}

	public ulong? EmojiId { get; init; }

	public string? EmojiName { get; init; }
}