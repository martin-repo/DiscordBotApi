// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyThreadArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels;

public class DiscordModifyThreadArgsBuilder
{
	private bool? _archived;

	public DiscordModifyThreadArgsBuilder WithArchived(bool? archived)
	{
		_archived = archived;
		return this;
	}

	public DiscordModifyThreadArgs Build() =>
		new()
		{
			Archived = _archived,
		};
}