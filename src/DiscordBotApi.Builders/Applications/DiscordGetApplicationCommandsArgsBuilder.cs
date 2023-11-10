// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGetApplicationCommandsArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Applications;

public class DiscordGetApplicationCommandsArgsBuilder
{
	private bool? _withLocalizations;

	public DiscordGetApplicationCommandsArgsBuilder WithWithLocalizations(bool? withLocalizations)
	{
		_withLocalizations = withLocalizations;
		return this;
	}

	public DiscordGetApplicationCommandsArgs Build() =>
		new()
		{
			WithLocalizations = _withLocalizations,
		};
}