// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordFieldBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages.Embeds;

public class DiscordFieldBuilder
{
	private bool? _inline;
	private string _name = default!;
	private string _value = default!;

	public DiscordFieldBuilder WithInline(bool? inline)
	{
		_inline = inline;
		return this;
	}

	public DiscordFieldBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordFieldBuilder WithValue(string value)
	{
		_value = value;
		return this;
	}

	public DiscordField Build() =>
		new()
		{
			Inline = _inline,
			Name = _name,
			Value = _value,
		};
}