// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackAutocompleteDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Interactions;
using DiscordBotApi.Models.Applications;

namespace DiscordBotApi.Models.Interactions;

internal sealed class DiscordInteractionCallbackAutocompleteDto : DiscordInteractionCallbackDataDto
{
	[JsonPropertyName(name: "choices")]
	public ImmutableArray<DiscordApplicationCommandOptionChoiceDto>? Choices { get; init; }

	public static DiscordInteractionCallbackAutocompleteDto FromModel(DiscordInteractionCallbackAutocomplete model) =>
		new()
		{
			Choices = model
				.Choices.Select(selector: DiscordApplicationCommandOptionChoiceDto.FromModel)
				.ToImmutableArray()
		};
}