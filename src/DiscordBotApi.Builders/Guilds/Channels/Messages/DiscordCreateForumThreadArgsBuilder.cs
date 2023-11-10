// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateForumThreadArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages;

public class DiscordCreateForumThreadArgsBuilder
{
	private List<ulong>? _appliedTags;
	private int? _autoArchiveDuration;
	private DiscordForumMessageArgs _message = default!;
	private string _name = default!;
	private int? _rateLimitPerUser;

	public DiscordCreateForumThreadArgsBuilder AddAppliedTag(ulong appliedTag)
	{
		_appliedTags ??= new List<ulong>();
		_appliedTags.Add(item: appliedTag);
		return this;
	}

	public DiscordCreateForumThreadArgsBuilder WithAutoArchiveDuration(int? autoArchiveDuration)
	{
		_autoArchiveDuration = autoArchiveDuration;
		return this;
	}

	public DiscordCreateForumThreadArgsBuilder WithMessage(DiscordForumMessageArgs message)
	{
		_message = message;
		return this;
	}

	public DiscordCreateForumThreadArgsBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordCreateForumThreadArgsBuilder WithRateLimitPerUser(int? rateLimitPerUser)
	{
		_rateLimitPerUser = rateLimitPerUser;
		return this;
	}

	public DiscordCreateForumThreadArgs Build() =>
		new()
		{
			AppliedTags = _appliedTags?.ToImmutableArray(),
			AutoArchiveDuration = _autoArchiveDuration,
			Message = _message,
			Name = _name,
			RateLimitPerUser = _rateLimitPerUser,
		};
}