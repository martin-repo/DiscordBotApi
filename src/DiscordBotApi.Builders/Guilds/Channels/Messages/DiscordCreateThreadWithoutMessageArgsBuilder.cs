// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateThreadWithoutMessageArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages;

public class DiscordCreateThreadWithoutMessageArgsBuilder
{
	private int? _autoArchiveDuration;
	private bool? _invitable;
	private string _name = default!;
	private int? _rateLimitPerUser;
	private DiscordChannelType? _type;

	public DiscordCreateThreadWithoutMessageArgsBuilder WithAutoArchiveDuration(int? autoArchiveDuration)
	{
		_autoArchiveDuration = autoArchiveDuration;
		return this;
	}

	public DiscordCreateThreadWithoutMessageArgsBuilder WithInvitable(bool? invitable)
	{
		_invitable = invitable;
		return this;
	}

	public DiscordCreateThreadWithoutMessageArgsBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordCreateThreadWithoutMessageArgsBuilder WithRateLimitPerUser(int? rateLimitPerUser)
	{
		_rateLimitPerUser = rateLimitPerUser;
		return this;
	}

	public DiscordCreateThreadWithoutMessageArgsBuilder WithType(DiscordChannelType? type)
	{
		_type = type;
		return this;
	}

	public DiscordCreateThreadWithoutMessageArgs Build() =>
		new()
		{
			AutoArchiveDuration = _autoArchiveDuration,
			Invitable = _invitable,
			Name = _name,
			RateLimitPerUser = _rateLimitPerUser,
			Type = _type,
		};
}