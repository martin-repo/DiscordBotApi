// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordImageBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages.Embeds;

public class DiscordImageBuilder
{
	private string _url = default!;

	public DiscordImageBuilder WithUrl(string url)
	{
		_url = url;
		return this;
	}

	public DiscordImage Build() =>
		new()
		{
			Url = _url,
		};
}