// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateMessageArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#create-message-jsonform-params
internal record DiscordCreateMessageArgsDto(
	[property: JsonPropertyName(name: "content")]
	string? Content,
	[property: JsonPropertyName(name: "embeds")]
	DiscordEmbedDto[]? Embeds,
	[property: JsonPropertyName(name: "message_reference")]
	DiscordMessageReferenceDto? MessageReference,
	[property: JsonPropertyName(name: "components")]
	DiscordMessageComponentDto[]? Components,
	[property: JsonPropertyName(name: "attachments")]
	DiscordMessageAttachmentDto[]? Attachments
)
{
	internal DiscordCreateMessageArgsDto(DiscordCreateMessageArgs model) : this(
		Content: model.Content,
		Embeds: model.Embeds?.Select(selector: e => new DiscordEmbedDto(model: e))
			.ToArray(),
		MessageReference: model.MessageReference != null
			? new DiscordMessageReferenceDto(model: model.MessageReference)
			: null,
		Components: model.Components?.Select(selector: DiscordMessageComponent.ConvertToDto)
			.ToArray(),
		Attachments: model.Attachments?.Select(selector: a => new DiscordMessageAttachmentDto(model: a))
			.ToArray())
	{
	}
}