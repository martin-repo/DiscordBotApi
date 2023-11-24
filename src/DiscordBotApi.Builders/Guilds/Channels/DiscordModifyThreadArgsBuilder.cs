// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyThreadArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels;

public class DiscordModifyThreadArgsBuilder
{
	private bool? _archived;
	private DiscordChannelFlags? _flags;
	private string? _name;

	public DiscordModifyThreadArgsBuilder WithArchived(bool? archived)
	{
		_archived = archived;
		return this;
	}

	public DiscordModifyThreadArgsBuilder WithFlags(DiscordChannelFlags? flags)
	{
		_flags = flags;
		return this;
	}

	public DiscordModifyThreadArgsBuilder WithName(string? name)
	{
		_name = name;
		return this;
	}

	public DiscordModifyThreadArgs Build() =>
		new()
		{
			Archived = _archived,
			Flags = _flags,
			Name = _name,
		};
}