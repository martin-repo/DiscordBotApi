// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordStartThreadWithMessageArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels;

public class DiscordStartThreadWithMessageArgsBuilder
{
	private AutoArchiveDuration? _autoArchiveDuration;
	private string _name = default!;

	public DiscordStartThreadWithMessageArgsBuilder WithAutoArchiveDuration(AutoArchiveDuration? autoArchiveDuration)
	{
		_autoArchiveDuration = autoArchiveDuration;
		return this;
	}

	public DiscordStartThreadWithMessageArgsBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordStartThreadWithMessageArgs Build() =>
		new()
		{
			AutoArchiveDuration = _autoArchiveDuration,
			Name = _name,
		};
}