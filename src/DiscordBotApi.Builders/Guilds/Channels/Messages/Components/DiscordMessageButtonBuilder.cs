// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageButtonBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages.Components;

public class DiscordMessageButtonBuilder
{
	private string? _customId;
	private bool? _disabled;
	private DiscordEmoji? _emoji;
	private string? _label;
	private DiscordMessageButtonStyle _style = default!;
	private string? _url;

	public DiscordMessageButtonBuilder WithCustomId(string? customId)
	{
		_customId = customId;
		return this;
	}

	public DiscordMessageButtonBuilder WithDisabled(bool? disabled)
	{
		_disabled = disabled;
		return this;
	}

	public DiscordMessageButtonBuilder WithEmoji(DiscordEmoji? emoji)
	{
		_emoji = emoji;
		return this;
	}

	public DiscordMessageButtonBuilder WithLabel(string? label)
	{
		_label = label;
		return this;
	}

	public DiscordMessageButtonBuilder WithStyle(DiscordMessageButtonStyle style)
	{
		_style = style;
		return this;
	}

	public DiscordMessageButtonBuilder WithUrl(string? url)
	{
		_url = url;
		return this;
	}

	public DiscordMessageButton Build() =>
		new()
		{
			CustomId = _customId,
			Disabled = _disabled,
			Emoji = _emoji,
			Label = _label,
			Style = _style,
			Url = _url,
		};
}