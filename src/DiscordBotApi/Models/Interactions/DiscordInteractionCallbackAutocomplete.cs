// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackAutocomplete.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Applications;

namespace DiscordBotApi.Models.Interactions;

public record DiscordInteractionCallbackAutocomplete : DiscordInteractionCallbackData
{
	public IReadOnlyCollection<DiscordApplicationCommandOptionChoice> Choices { get; init; } = default!;
}