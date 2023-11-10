// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageTextInputBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages.Components;

public class DiscordMessageTextInputBuilder
{
	private string _customId = default!;
	private string _label = default!;
	private int? _maxLength;
	private int? _minLength;
	private string? _placeholder;
	private bool? _required;
	private DiscordMessageTextInputStyle _style = default!;
	private string? _value;

	public DiscordMessageTextInputBuilder WithCustomId(string customId)
	{
		_customId = customId;
		return this;
	}

	public DiscordMessageTextInputBuilder WithLabel(string label)
	{
		_label = label;
		return this;
	}

	public DiscordMessageTextInputBuilder WithMaxLength(int? maxLength)
	{
		_maxLength = maxLength;
		return this;
	}

	public DiscordMessageTextInputBuilder WithMinLength(int? minLength)
	{
		_minLength = minLength;
		return this;
	}

	public DiscordMessageTextInputBuilder WithPlaceholder(string? placeholder)
	{
		_placeholder = placeholder;
		return this;
	}

	public DiscordMessageTextInputBuilder WithRequired(bool? required)
	{
		_required = required;
		return this;
	}

	public DiscordMessageTextInputBuilder WithStyle(DiscordMessageTextInputStyle style)
	{
		_style = style;
		return this;
	}

	public DiscordMessageTextInputBuilder WithValue(string? value)
	{
		_value = value;
		return this;
	}

	public DiscordMessageTextInput Build() =>
		new()
		{
			CustomId = _customId,
			Label = _label,
			MaxLength = _maxLength,
			MinLength = _minLength,
			Placeholder = _placeholder,
			Required = _required,
			Style = _style,
			Value = _value,
		};
}