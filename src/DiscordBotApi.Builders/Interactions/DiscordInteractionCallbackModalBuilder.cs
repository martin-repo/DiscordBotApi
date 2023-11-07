// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackModalBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Interactions;

public class DiscordInteractionCallbackModalBuilder
{
	private readonly List<DiscordMessageComponent> _components = new();
	private string _customId = default!;
	private string _title = default!;

	public DiscordInteractionCallbackModalBuilder AddActionRow(Action<DiscordMessageActionRowBuilder> builderAction)
	{
		var builder = new DiscordMessageActionRowBuilder();
		builderAction(obj: builder);
		_components.Add(item: builder.Build());
		return this;
	}

	public DiscordInteractionCallbackModalBuilder WithCustomId(string customId)
	{
		_customId = customId;
		return this;
	}

	public DiscordInteractionCallbackModalBuilder WithTitle(string title)
	{
		_title = title;
		return this;
	}

	public DiscordInteractionCallbackModal Build() =>
		new()
		{
			Components = _components.ToImmutableArray(),
			CustomId = _customId,
			Title = _title,
		};
}