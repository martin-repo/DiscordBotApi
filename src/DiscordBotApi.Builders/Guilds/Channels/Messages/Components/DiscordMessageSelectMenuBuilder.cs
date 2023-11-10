// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageSelectMenuBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages.Components;

public class DiscordMessageSelectMenuBuilder
{
	private string _customId = default!;
	private bool? _disabled;
	private int? _maxValues;
	private int? _minValues;
	private List<DiscordMessageSelectMenuOption>? _options;
	private string? _placeholder;

	public DiscordMessageSelectMenuBuilder WithCustomId(string customId)
	{
		_customId = customId;
		return this;
	}

	public DiscordMessageSelectMenuBuilder WithDisabled(bool? disabled)
	{
		_disabled = disabled;
		return this;
	}

	public DiscordMessageSelectMenuBuilder WithMaxValues(int? maxValues)
	{
		_maxValues = maxValues;
		return this;
	}

	public DiscordMessageSelectMenuBuilder WithMinValues(int? minValues)
	{
		_minValues = minValues;
		return this;
	}

	public DiscordMessageSelectMenuBuilder AddOption(Action<DiscordMessageSelectMenuOptionBuilder> builderAction)
	{
		var builder = new DiscordMessageSelectMenuOptionBuilder();
		builderAction(obj: builder);
		_options ??= new List<DiscordMessageSelectMenuOption>();
		_options.Add(item: builder.Build());
		return this;
	}

	public DiscordMessageSelectMenuBuilder WithPlaceholder(string? placeholder)
	{
		_placeholder = placeholder;
		return this;
	}

	public DiscordMessageSelectMenu Build() =>
		new()
		{
			CustomId = _customId,
			Disabled = _disabled,
			MaxValues = _maxValues,
			MinValues = _minValues,
			Options = _options?.ToImmutableArray(),
			Placeholder = _placeholder,
		};
}