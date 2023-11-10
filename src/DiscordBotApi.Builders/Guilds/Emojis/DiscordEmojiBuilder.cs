// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEmojiBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Emojis;

public class DiscordEmojiBuilder
{
	private bool? _animated;
	private bool? _available;
	private ulong? _id;
	private string? _name;
	private bool? _requireColons;

	public DiscordEmojiBuilder WithAnimated(bool? animated)
	{
		_animated = animated;
		return this;
	}

	public DiscordEmojiBuilder WithAvailable(bool? available)
	{
		_available = available;
		return this;
	}

	public DiscordEmojiBuilder WithId(ulong? id)
	{
		_id = id;
		return this;
	}

	public DiscordEmojiBuilder WithName(string? name)
	{
		_name = name;
		return this;
	}

	public DiscordEmojiBuilder WithRequireColons(bool? requireColons)
	{
		_requireColons = requireColons;
		return this;
	}

	public DiscordEmoji Build() =>
		new()
		{
			Animated = _animated,
			Available = _available,
			Id = _id,
			Name = _name,
			RequireColons = _requireColons,
		};
}