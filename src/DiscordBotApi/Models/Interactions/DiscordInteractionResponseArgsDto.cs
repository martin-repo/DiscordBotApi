// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionResponseArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Interactions;

namespace DiscordBotApi.Models.Interactions;

internal sealed record DiscordInteractionResponseArgsDto(
	[property: JsonPropertyName(name: "type")]
	int Type,
	[property: JsonPropertyName(name: "data")]
	DiscordInteractionCallbackDataDto? Data
)
{
	public static DiscordInteractionResponseArgsDto FromModel(DiscordInteractionResponseArgs model) =>
		new(Type: (int)model.Type, Data: FromModel(model: model.Data));

	private static DiscordInteractionCallbackDataDto? FromModel(DiscordInteractionCallbackData? model) =>
		model switch
		{
			DiscordInteractionCallbackMessage message => DiscordInteractionCallbackMessageDto.FromModel(model: message),
			DiscordInteractionCallbackAutocomplete autocomplete => DiscordInteractionCallbackAutocompleteDto.FromModel(
				model: autocomplete
			),
			DiscordInteractionCallbackModal modal => DiscordInteractionCallbackModalDto.FromModel(model: modal),
			null => null,
			_ => throw new NotSupportedException(
				message: $"{typeof(DiscordInteractionCallbackData)} {model.GetType().Name} is not supported"
			)
		};
}