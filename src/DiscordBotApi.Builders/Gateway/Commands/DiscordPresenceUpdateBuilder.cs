// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPresenceUpdateBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Gateway.Commands;

public class DiscordPresenceUpdateBuilder
{
	private readonly List<DiscordActivityUpdate> _activities = new();
	private bool _afk = default!;
	private DateTime? _since;
	private DiscordPresenceStatus _status = default!;

	public DiscordPresenceUpdateBuilder AddActivity(Action<DiscordActivityUpdateBuilder> builderAction)
	{
		var builder = new DiscordActivityUpdateBuilder();
		builderAction(obj: builder);
		_activities.Add(item: builder.Build());
		return this;
	}

	public DiscordPresenceUpdateBuilder AddActivity(DiscordActivityUpdate item)
	{
		_activities.Add(item: item);
		return this;
	}

	public DiscordPresenceUpdateBuilder WithAfk(bool afk)
	{
		_afk = afk;
		return this;
	}

	public DiscordPresenceUpdateBuilder WithSince(DateTime? since)
	{
		_since = since;
		return this;
	}

	public DiscordPresenceUpdateBuilder WithStatus(DiscordPresenceStatus status)
	{
		_status = status;
		return this;
	}

	public DiscordPresenceUpdate Build() =>
		new()
		{
			Activities = _activities.ToImmutableArray(),
			Afk = _afk,
			Since = _since,
			Status = _status,
		};
}