// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionResponseArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Interactions;

internal record DiscordInteractionResponseArgsDto(
	[property: JsonPropertyName(name: "type")]
	int Type,
	[property: JsonPropertyName(name: "data")]
	DiscordInteractionCallbackDataDto? Data
)
{
	internal DiscordInteractionResponseArgsDto(DiscordInteractionResponseArgs model) : this(
		Type: (int)model.Type,
		Data: ConvertToDto(model: model.Data))
	{
	}

	private static DiscordInteractionCallbackDataDto? ConvertToDto(DiscordInteractionCallbackData? model)
	{
		if (model == null)
		{
			return null;
		}

		switch (model)
		{
			case DiscordInteractionCallbackMessage message:
				return new DiscordInteractionCallbackMessageDto(model: message);
			case DiscordInteractionCallbackAutocomplete autocomplete:
				return new DiscordInteractionCallbackAutocompleteDto(model: autocomplete);
			default:
				throw new NotSupportedException(
					message: $"{typeof(DiscordInteractionCallbackData)} {model.GetType().Name} is not supported");
		}
	}
}