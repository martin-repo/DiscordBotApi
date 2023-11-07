// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildChannelArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds;

public class DiscordCreateGuildChannelArgsBuilder
{
	private List<DiscordForumTag>? _availableTags;
	private int? _defaultAutoArchiveDuration;
	private DiscordForumLayoutType? _defaultForumLayout;
	private DiscordDefaultReaction? _defaultReactionEmoji;
	private DiscordSortOrderType? _defaultSortOrder;
	private string _name = default!;
	private ulong? _parentId;
	private List<DiscordPermissionOverwrite>? _permissionOverwrites;
	private int? _position;
	private string? _topic;
	private DiscordChannelType? _type;

	public DiscordCreateGuildChannelArgsBuilder AddAvailableTag(Action<DiscordForumTagBuilder> builderAction)
	{
		var builder = new DiscordForumTagBuilder();
		builderAction(obj: builder);
		_availableTags ??= new List<DiscordForumTag>();
		_availableTags.Add(item: builder.Build());
		return this;
	}

	public DiscordCreateGuildChannelArgsBuilder WithDefaultAutoArchiveDuration(int? defaultAutoArchiveDuration)
	{
		_defaultAutoArchiveDuration = defaultAutoArchiveDuration;
		return this;
	}

	public DiscordCreateGuildChannelArgsBuilder WithDefaultForumLayout(DiscordForumLayoutType? defaultForumLayout)
	{
		_defaultForumLayout = defaultForumLayout;
		return this;
	}

	public DiscordCreateGuildChannelArgsBuilder WithDefaultReactionEmoji(DiscordDefaultReaction? defaultReactionEmoji)
	{
		_defaultReactionEmoji = defaultReactionEmoji;
		return this;
	}

	public DiscordCreateGuildChannelArgsBuilder WithDefaultSortOrder(DiscordSortOrderType? defaultSortOrder)
	{
		_defaultSortOrder = defaultSortOrder;
		return this;
	}

	public DiscordCreateGuildChannelArgsBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordCreateGuildChannelArgsBuilder WithParentId(ulong? parentId)
	{
		_parentId = parentId;
		return this;
	}

	public DiscordCreateGuildChannelArgsBuilder AddPermissionOverwrite(Action<DiscordPermissionOverwriteBuilder> builderAction)
	{
		var builder = new DiscordPermissionOverwriteBuilder();
		builderAction(obj: builder);
		_permissionOverwrites ??= new List<DiscordPermissionOverwrite>();
		_permissionOverwrites.Add(item: builder.Build());
		return this;
	}

	public DiscordCreateGuildChannelArgsBuilder WithPosition(int? position)
	{
		_position = position;
		return this;
	}

	public DiscordCreateGuildChannelArgsBuilder WithTopic(string? topic)
	{
		_topic = topic;
		return this;
	}

	public DiscordCreateGuildChannelArgsBuilder WithType(DiscordChannelType? type)
	{
		_type = type;
		return this;
	}

	public DiscordCreateGuildChannelArgs Build() =>
		new()
		{
			AvailableTags = _availableTags?.ToImmutableArray(),
			DefaultAutoArchiveDuration = _defaultAutoArchiveDuration,
			DefaultForumLayout = _defaultForumLayout,
			DefaultReactionEmoji = _defaultReactionEmoji,
			DefaultSortOrder = _defaultSortOrder,
			Name = _name,
			ParentId = _parentId,
			PermissionOverwrites = _permissionOverwrites?.ToImmutableArray(),
			Position = _position,
			Topic = _topic,
			Type = _type,
		};
}