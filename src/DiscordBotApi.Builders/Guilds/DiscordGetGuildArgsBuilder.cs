// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGetGuildArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds;

public class DiscordGetGuildArgsBuilder
{
	private bool? _withCounts;

	public DiscordGetGuildArgsBuilder WithWithCounts(bool? withCounts)
	{
		_withCounts = withCounts;
		return this;
	}

	public DiscordGetGuildArgs Build() =>
		new()
		{
			WithCounts = _withCounts,
		};
}