// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageActionRowDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

// https://discord.com/developers/docs/interactions/message-components#action-rows
internal record DiscordMessageActionRowDto(
	[property: JsonPropertyName(name: "components")]
	DiscordMessageComponentDto[] Components
) : DiscordMessageComponentDto(Type: (int)DiscordMessageComponentType.ActionRow)
{
	internal DiscordMessageActionRowDto(DiscordMessageActionRow model) : this(
		Components: model.Components.Select(selector: DiscordMessageComponent.ConvertToDto)
			.ToArray())
	{
	}
}