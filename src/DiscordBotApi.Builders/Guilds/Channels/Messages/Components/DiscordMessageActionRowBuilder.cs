// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageActionRowBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages.Components;

public class DiscordMessageActionRowBuilder
{
	private readonly List<DiscordMessageComponent> _components = new();

	public DiscordMessageActionRowBuilder AddActionRow(Action<DiscordMessageActionRowBuilder> builderAction)
	{
		var builder = new DiscordMessageActionRowBuilder();
		builderAction(obj: builder);
		_components.Add(item: builder.Build());
		return this;
	}

	public DiscordMessageActionRowBuilder AddButton(Action<DiscordMessageButtonBuilder> builderAction)
	{
		var builder = new DiscordMessageButtonBuilder();
		builderAction(obj: builder);
		_components.Add(item: builder.Build());
		return this;
	}

	public DiscordMessageActionRowBuilder AddButton(DiscordMessageButton button)
	{
		_components.Add(item: button);
		return this;
	}

	public DiscordMessageActionRowBuilder AddSelectMenu(Action<DiscordMessageSelectMenuBuilder> builderAction)
	{
		var builder = new DiscordMessageSelectMenuBuilder();
		builderAction(obj: builder);
		_components.Add(item: builder.Build());
		return this;
	}

	public DiscordMessageActionRowBuilder AddSelectMenu(DiscordMessageSelectMenu selectMenu)
	{
		_components.Add(item: selectMenu);
		return this;
	}

	public DiscordMessageActionRowBuilder AddTextInput(Action<DiscordMessageTextInputBuilder> builderAction)
	{
		var builder = new DiscordMessageTextInputBuilder();
		builderAction(obj: builder);
		_components.Add(item: builder.Build());
		return this;
	}

	public DiscordMessageActionRowBuilder AddButton(DiscordMessageTextInput textInput)
	{
		_components.Add(item: textInput);
		return this;
	}

	public DiscordMessageActionRow Build() =>
		new()
		{
			Components = _components.ToImmutableArray(),
		};
}