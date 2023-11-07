// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordFooterBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages.Embeds;

public class DiscordFooterBuilder
{
	private string? _iconUrl;
	private string _text = default!;

	public DiscordFooterBuilder WithIconUrl(string? iconUrl)
	{
		_iconUrl = iconUrl;
		return this;
	}

	public DiscordFooterBuilder WithText(string text)
	{
		_text = text;
		return this;
	}

	public DiscordFooter Build() =>
		new()
		{
			IconUrl = _iconUrl,
			Text = _text,
		};
}