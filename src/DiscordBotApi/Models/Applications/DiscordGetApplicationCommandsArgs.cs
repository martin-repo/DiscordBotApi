// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGetApplicationCommandsArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications;

public record DiscordGetApplicationCommandsArgs
{
	public bool? WithLocalizations { get; init; }
}