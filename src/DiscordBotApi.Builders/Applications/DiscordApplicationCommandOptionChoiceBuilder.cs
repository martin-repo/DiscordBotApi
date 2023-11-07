// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOptionChoiceBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Applications;

public class DiscordApplicationCommandOptionChoiceBuilder
{
	private string _name = default!;
	private Object _value = default!;

	public DiscordApplicationCommandOptionChoiceBuilder WithName(string name)
	{
		_name = name;
		return this;
	}

	public DiscordApplicationCommandOptionChoiceBuilder WithValue(Object value)
	{
		_value = value;
		return this;
	}

	public DiscordApplicationCommandOptionChoice Build() =>
		new()
		{
			Name = _name,
			Value = _value,
		};
}