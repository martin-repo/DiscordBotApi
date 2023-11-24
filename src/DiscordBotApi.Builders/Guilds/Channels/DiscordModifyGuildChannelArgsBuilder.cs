// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyGuildChannelArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels;

public class DiscordModifyGuildChannelArgsBuilder
{
	private List<DiscordForumTag>? _availableTags;
	private int? _defaultAutoArchiveDuration;
	private DiscordForumLayoutType? _defaultForumLayout;
	private DiscordDefaultReaction? _defaultReactionEmoji;
	private DiscordSortOrderType? _defaultSortOrder;
	private string? _name;
	private ulong? _parentId;
	private List<DiscordPermissionOverwrite>? _permissionOverwrites;
	private int? _position;
	private string? _topic;
	private DiscordChannelType? _type;

	public DiscordModifyGuildChannelArgsBuilder AddAvailableTag(Action<DiscordForumTagBuilder> builderAction)
	{
		var builder = new DiscordForumTagBuilder();
		builderAction(obj: builder);
		_availableTags ??= new List<DiscordForumTag>();
		_availableTags.Add(item: builder.Build());
		return this;
	}

	public DiscordModifyGuildChannelArgsBuilder AddAvailableTag(DiscordForumTag item)
	{
		_availableTags ??= new List<DiscordForumTag>();
		_availableTags.Add(item: item);
		return this;
	}

	public DiscordModifyGuildChannelArgsBuilder WithDefaultAutoArchiveDuration(int? defaultAutoArchiveDuration)
	{
		_defaultAutoArchiveDuration = defaultAutoArchiveDuration;
		return this;
	}

	public DiscordModifyGuildChannelArgsBuilder WithDefaultForumLayout(DiscordForumLayoutType? defaultForumLayout)
	{
		_defaultForumLayout = defaultForumLayout;
		return this;
	}

	public DiscordModifyGuildChannelArgsBuilder WithDefaultReactionEmoji(DiscordDefaultReaction? defaultReactionEmoji)
	{
		_defaultReactionEmoji = defaultReactionEmoji;
		return this;
	}

	public DiscordModifyGuildChannelArgsBuilder WithDefaultSortOrder(DiscordSortOrderType? defaultSortOrder)
	{
		_defaultSortOrder = defaultSortOrder;
		return this;
	}

	public DiscordModifyGuildChannelArgsBuilder WithName(string? name)
	{
		_name = name;
		return this;
	}

	public DiscordModifyGuildChannelArgsBuilder WithParentId(ulong? parentId)
	{
		_parentId = parentId;
		return this;
	}

	public DiscordModifyGuildChannelArgsBuilder AddPermissionOverwrite(Action<DiscordPermissionOverwriteBuilder> builderAction)
	{
		var builder = new DiscordPermissionOverwriteBuilder();
		builderAction(obj: builder);
		_permissionOverwrites ??= new List<DiscordPermissionOverwrite>();
		_permissionOverwrites.Add(item: builder.Build());
		return this;
	}

	public DiscordModifyGuildChannelArgsBuilder AddPermissionOverwrite(DiscordPermissionOverwrite item)
	{
		_permissionOverwrites ??= new List<DiscordPermissionOverwrite>();
		_permissionOverwrites.Add(item: item);
		return this;
	}

	public DiscordModifyGuildChannelArgsBuilder WithPosition(int? position)
	{
		_position = position;
		return this;
	}

	public DiscordModifyGuildChannelArgsBuilder WithTopic(string? topic)
	{
		_topic = topic;
		return this;
	}

	public DiscordModifyGuildChannelArgsBuilder WithType(DiscordChannelType? type)
	{
		_type = type;
		return this;
	}

	public DiscordModifyGuildChannelArgs Build() =>
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