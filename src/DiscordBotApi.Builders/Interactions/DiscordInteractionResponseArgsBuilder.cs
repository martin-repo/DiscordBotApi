// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionResponseArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Interactions;

public class DiscordInteractionResponseArgsBuilder
{
	private DiscordInteractionCallbackData? _data;
	private DiscordInteractionCallbackType _type = default!;

	public DiscordInteractionResponseArgsBuilder WithData(DiscordInteractionCallbackData? data)
	{
		_data = data;
		return this;
	}

	public DiscordInteractionResponseArgsBuilder WithType(DiscordInteractionCallbackType type)
	{
		_type = type;
		return this;
	}

	public DiscordInteractionResponseArgs Build() =>
		new()
		{
			Data = _data,
			Type = _type,
		};
}