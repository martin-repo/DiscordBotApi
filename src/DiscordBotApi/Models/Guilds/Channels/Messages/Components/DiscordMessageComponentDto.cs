// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageComponentDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

[JsonConverter(converterType: typeof(DiscordMessageComponentDtoConverter))]
internal abstract record DiscordMessageComponentDto(
	[property: JsonPropertyName(name: "type")]
	int Type
)
{
	public virtual DiscordMessageComponent ToModel() =>
		this switch
		{
			DiscordMessageActionRowDto actionRowDto => actionRowDto.ToModel(),
			DiscordMessageButtonDto buttonDto => buttonDto.ToModel(),
			DiscordMessageSelectMenuDto menu => menu.ToModel(),
			DiscordMessageTextInputDto textInput => textInput.ToModel(),
			_ => throw new NotSupportedException(
				message: $"{typeof(DiscordMessageComponent)} {GetType().Name} is not supported"
			)
		};

	public static DiscordMessageComponentDto FromModel(DiscordMessageComponent model) =>
		model switch
		{
			DiscordMessageActionRow actionRow => DiscordMessageActionRowDto.FromModel(model: actionRow),
			DiscordMessageButton button => DiscordMessageButtonDto.FromModel(model: button),
			DiscordMessageSelectMenu menu => DiscordMessageSelectMenuDto.FromModel(model: menu),
			DiscordMessageTextInput textInput => DiscordMessageTextInputDto.FromModel(model: textInput),
			_ => throw new NotSupportedException(
				message: $"{typeof(DiscordMessageComponent)} {model.GetType().Name} is not supported"
			)
		};
}