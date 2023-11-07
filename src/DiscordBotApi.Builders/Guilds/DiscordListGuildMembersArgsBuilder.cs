// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordListGuildMembersArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds;

public class DiscordListGuildMembersArgsBuilder
{
	private ulong? _after;
	private int? _limit;

	public DiscordListGuildMembersArgsBuilder WithAfter(ulong? after)
	{
		_after = after;
		return this;
	}

	public DiscordListGuildMembersArgsBuilder WithLimit(int? limit)
	{
		_limit = limit;
		return this;
	}

	public DiscordListGuildMembersArgs Build() =>
		new()
		{
			After = _after,
			Limit = _limit,
		};
}