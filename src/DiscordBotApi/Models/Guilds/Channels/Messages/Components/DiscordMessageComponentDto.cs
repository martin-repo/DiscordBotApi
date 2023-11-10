// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageComponentDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

[JsonConverter(converterType: typeof(DiscordMessageComponentDtoConverter))]
internal abstract record DiscordMessageComponentDto(
	[property: JsonPropertyName(name: "type")]
	int Type
)
{
	internal static DiscordMessageComponent ConvertToModel(DiscordMessageComponentDto dto)
	{
		switch (dto)
		{
			case DiscordMessageActionRowDto actionRowDto:
				return new DiscordMessageActionRow(dto: actionRowDto);
			case DiscordMessageButtonDto buttonDto:
				return new DiscordMessageButton(dto: buttonDto);
			case DiscordMessageSelectMenuDto menu:
				return new DiscordMessageSelectMenu(dto: menu);
			case DiscordMessageTextInputDto textInput:
				return new DiscordMessageTextInput(dto: textInput);
			default:
				throw new NotSupportedException(message: $"{typeof(DiscordMessageComponent)} {dto.GetType().Name} is not supported");
		}
	}
}