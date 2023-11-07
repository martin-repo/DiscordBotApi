// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordListPublicArchivedThreadsArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels;

public class DiscordListPublicArchivedThreadsArgsBuilder
{
	private DateTime? _before;
	private int? _limit;

	public DiscordListPublicArchivedThreadsArgsBuilder WithBefore(DateTime? before)
	{
		_before = before;
		return this;
	}

	public DiscordListPublicArchivedThreadsArgsBuilder WithLimit(int? limit)
	{
		_limit = limit;
		return this;
	}

	public DiscordListPublicArchivedThreadsArgs Build() =>
		new()
		{
			Before = _before,
			Limit = _limit,
		};
}