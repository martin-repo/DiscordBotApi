// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackAutocomplete.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Applications;

namespace DiscordBotApi.Interface.Models.Interactions;

public sealed class DiscordInteractionCallbackAutocomplete : DiscordInteractionCallbackData
{
	public required IReadOnlyCollection<DiscordApplicationCommandOptionChoice> Choices { get; init; }
}