// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackAutocompleteBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Interactions;

public class DiscordInteractionCallbackAutocompleteBuilder
{
	private readonly List<DiscordApplicationCommandOptionChoice> _choices = new();

	public DiscordInteractionCallbackAutocompleteBuilder AddChoice(Action<DiscordApplicationCommandOptionChoiceBuilder> builderAction)
	{
		var builder = new DiscordApplicationCommandOptionChoiceBuilder();
		builderAction(obj: builder);
		_choices.Add(item: builder.Build());
		return this;
	}

	public DiscordInteractionCallbackAutocompleteBuilder AddChoice(DiscordApplicationCommandOptionChoice item)
	{
		_choices.Add(item: item);
		return this;
	}

	public DiscordInteractionCallbackAutocomplete Build() =>
		new()
		{
			Choices = _choices.ToImmutableArray(),
		};
}