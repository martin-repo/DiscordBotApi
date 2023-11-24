// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOptionBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Applications;

public class DiscordApplicationCommandOptionBuilder
{
	private bool? _autocomplete;
	private List<DiscordChannelType>? _channelTypes;
	private List<DiscordApplicationCommandOptionChoice>? _choices;
	private string _description = default!;
	private int? _maxLength;
	private Object? _maxValue;
	private int? _minLength;
	private Object? _minValue;
	private string _name = default!;
	private List<DiscordApplicationCommandOption>? _options;
	private bool? _required;
	private DiscordApplicationCommandOptionType _type = default!;

	public DiscordApplicationCommandOptionBuilder WithAutocomplete(bool? autocomplete)
	{
		_autocomplete = autocomplete;
		return this;
	}

	public DiscordApplicationCommandOptionBuilder AddChannelType(DiscordChannelType channelType)
	{
		_channelTypes ??= new List<DiscordChannelType>();
		_channelTypes.Add(item: channelType);
		return this;
	}

	public DiscordApplicationCommandOptionBuilder AddChoice(Action<DiscordApplicationCommandOptionChoiceBuilder> builderAction)
	{
		var builder = new DiscordApplicationCommandOptionChoiceBuilder();
		builderAction(obj: builder);
		_choices ??= new List<DiscordApplicationCommandOptionChoice>();
		_choices.Add(item: builder.Build());
		return this;
	}

	public DiscordApplicationCommandOptionBuilder AddChoice(DiscordApplicationCommandOptionChoice item)
	{
		_choices ??= new List<DiscordApplicationCommandOptionChoice>();
		_choices.Add(item: item);
		return this;
	}

	public DiscordApplicationCommandOptionBuilder WithDescription(string description)
	{
		_description = description;
		return this;
	}

	public DiscordApplicationCommandOptionBuilder WithMaxLength(int? maxLength)
	{
		_maxLength = maxLength;
		return this;
	}

	public DiscordApplicationCommandOptionBuilder WithMaxValue(Object? maxValue)
	{
		_maxValue = maxValue;
		return this;
	}

	public DiscordApplicationCommandOptionBuilder WithMinLength(int? minLength)
	{
		_minLength = minLength;
		return this;
	}

	public DiscordApplicationCommandOptionBuilder WithMinValue(Object? minValue)
	{
		_minValue = minValue;
		return this;
	}

	public DiscordApplicationCommandOptionBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordApplicationCommandOptionBuilder AddOption(Action<DiscordApplicationCommandOptionBuilder> builderAction)
	{
		var builder = new DiscordApplicationCommandOptionBuilder();
		builderAction(obj: builder);
		_options ??= new List<DiscordApplicationCommandOption>();
		_options.Add(item: builder.Build());
		return this;
	}

	public DiscordApplicationCommandOptionBuilder AddOption(DiscordApplicationCommandOption item)
	{
		_options ??= new List<DiscordApplicationCommandOption>();
		_options.Add(item: item);
		return this;
	}

	public DiscordApplicationCommandOptionBuilder WithRequired(bool? required)
	{
		_required = required;
		return this;
	}

	public DiscordApplicationCommandOptionBuilder WithType(DiscordApplicationCommandOptionType type)
	{
		_type = type;
		return this;
	}

	public DiscordApplicationCommandOption Build() =>
		new()
		{
			Autocomplete = _autocomplete,
			ChannelTypes = _channelTypes?.ToImmutableArray(),
			Choices = _choices?.ToImmutableArray(),
			Description = _description,
			MaxLength = _maxLength,
			MaxValue = _maxValue,
			MinLength = _minLength,
			MinValue = _minValue,
			Name = _name,
			Options = _options?.ToImmutableArray(),
			Required = _required,
			Type = _type,
		};
}