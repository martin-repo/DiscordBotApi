// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackModalDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Models.Interactions;

// https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-response-object-modal
internal record DiscordInteractionCallbackModalDto(
	[property: JsonPropertyName(name: "custom_id")]
	string CustomId,
	[property: JsonPropertyName(name: "title")]
	string Title,
	[property: JsonPropertyName(name: "components")]
	DiscordMessageComponentDto[] Components
) : DiscordInteractionCallbackDataDto
{
	internal DiscordInteractionCallbackModalDto(DiscordInteractionCallbackModal model) : this(
		CustomId: model.CustomId,
		Title: model.Title,
		Components: model.Components.Select(selector: DiscordMessageComponent.ConvertToDto)
			.ToArray())
	{
	}
}