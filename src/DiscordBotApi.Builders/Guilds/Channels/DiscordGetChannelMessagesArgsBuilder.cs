// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGetChannelMessagesArgsBuilder.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels;

// WARNING! This file was generated by a tool.
//          Any changes made to this file will be lost if the code is regenerated.
public class DiscordGetChannelMessagesArgsBuilder
{
	private ulong? _after;
	private ulong? _around;
	private ulong? _before;
	private int? _limit;

	public DiscordGetChannelMessagesArgsBuilder WithAfter(ulong? after)
	{
		_after = after;
		return this;
	}

	public DiscordGetChannelMessagesArgsBuilder WithAround(ulong? around)
	{
		_around = around;
		return this;
	}

	public DiscordGetChannelMessagesArgsBuilder WithBefore(ulong? before)
	{
		_before = before;
		return this;
	}

	public DiscordGetChannelMessagesArgsBuilder WithLimit(int? limit)
	{
		_limit = limit;
		return this;
	}

	public DiscordGetChannelMessagesArgs Build() =>
		new()
		{
			After = _after,
			Around = _around,
			Before = _before,
			Limit = _limit,
		};
}