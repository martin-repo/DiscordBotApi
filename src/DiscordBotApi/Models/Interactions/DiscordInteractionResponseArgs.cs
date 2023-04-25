// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionResponseArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions;

public record DiscordInteractionResponseArgs
{
	public DiscordInteractionCallbackData? Data { get; init; }

	public DiscordInteractionCallbackType Type { get; init; }
}