// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackAutocompleteDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Applications;

namespace DiscordBotApi.Models.Interactions;

internal record DiscordInteractionCallbackAutocompleteDto(
	[property: JsonPropertyName(name: "choices")]
	DiscordApplicationCommandOptionChoiceDto[]? Choices
) : DiscordInteractionCallbackDataDto
{
	internal DiscordInteractionCallbackAutocompleteDto(DiscordInteractionCallbackAutocomplete model) : this(
		Choices: model.Choices.Select(selector: c => new DiscordApplicationCommandOptionChoiceDto(model: c))
			.ToArray())
	{
	}
}