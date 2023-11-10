// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordAuthorBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages.Embeds;

public class DiscordAuthorBuilder
{
	private string? _iconUrl;
	private string _name = default!;

	public DiscordAuthorBuilder WithIconUrl(string? iconUrl)
	{
		_iconUrl = iconUrl;
		return this;
	}

	public DiscordAuthorBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordAuthor Build() =>
		new()
		{
			IconUrl = _iconUrl,
			Name = _name,
		};
}