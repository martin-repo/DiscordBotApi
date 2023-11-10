// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordForumTagBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels;

public class DiscordForumTagBuilder
{
	private ulong? _emojiId;
	private string? _emojiName;
	private ulong? _id;
	private bool? _moderated;
	private string _name = default!;

	public DiscordForumTagBuilder WithEmojiId(ulong? emojiId)
	{
		_emojiId = emojiId;
		return this;
	}

	public DiscordForumTagBuilder WithEmojiName(string? emojiName)
	{
		_emojiName = emojiName;
		return this;
	}

	public DiscordForumTagBuilder WithId(ulong? id)
	{
		_id = id;
		return this;
	}

	public DiscordForumTagBuilder WithModerated(bool? moderated)
	{
		_moderated = moderated;
		return this;
	}

	public DiscordForumTagBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordForumTag Build() =>
		new()
		{
			EmojiId = _emojiId,
			EmojiName = _emojiName,
			Id = _id,
			Moderated = _moderated,
			Name = _name,
		};
}