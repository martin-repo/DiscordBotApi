// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThumbnailBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages.Embeds;

public class DiscordThumbnailBuilder
{
	private string _url = default!;

	public DiscordThumbnailBuilder WithUrl(string url)
	{
		_url = url;
		return this;
	}

	public DiscordThumbnail Build() =>
		new()
		{
			Url = _url,
		};
}