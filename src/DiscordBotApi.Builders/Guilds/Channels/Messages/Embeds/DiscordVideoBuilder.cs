// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordVideoBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages.Embeds;

public class DiscordVideoBuilder
{
	private string? _url;

	public DiscordVideoBuilder WithUrl(string? url)
	{
		_url = url;
		return this;
	}

	public DiscordVideo Build() =>
		new()
		{
			Url = _url,
		};
}