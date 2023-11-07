// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordDefaultReactionBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels;

public class DiscordDefaultReactionBuilder
{
	private ulong? _emojiId;
	private string? _emojiName;

	public DiscordDefaultReactionBuilder WithEmojiId(ulong? emojiId)
	{
		_emojiId = emojiId;
		return this;
	}

	public DiscordDefaultReactionBuilder WithEmojiName(string? emojiName)
	{
		_emojiName = emojiName;
		return this;
	}

	public DiscordDefaultReaction Build() =>
		new()
		{
			EmojiId = _emojiId,
			EmojiName = _emojiName,
		};
}