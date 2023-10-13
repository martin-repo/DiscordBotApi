// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackMessageDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Models.Interactions;

internal record DiscordInteractionCallbackMessageDto(
	[property: JsonPropertyName(name: "content")]
	string? Content,
	[property: JsonPropertyName(name: "embeds")]
	DiscordEmbedDto[]? Embeds,
	[property: JsonPropertyName(name: "flags")]
	ulong? Flags,
	[property: JsonPropertyName(name: "components")]
	DiscordMessageComponentDto[]? Components,
	[property: JsonPropertyName(name: "attachments")]
	DiscordMessageAttachmentDto[]? Attachments
) : DiscordInteractionCallbackDataDto
{
	internal DiscordInteractionCallbackMessageDto(DiscordInteractionCallbackMessage model) : this(
		Content: model.Content,
		Embeds: model.Embeds?.Select(selector: e => new DiscordEmbedDto(model: e))
			.ToArray(),
		Flags: model.Flags != null
			? (ulong)model.Flags
			: null,
		Components: model.Components?.Select(selector: DiscordMessageComponent.ConvertToDto)
			.ToArray(),
		Attachments: model.Attachments?.Select(selector: a => new DiscordMessageAttachmentDto(model: a))
			.ToArray())
	{
	}
}