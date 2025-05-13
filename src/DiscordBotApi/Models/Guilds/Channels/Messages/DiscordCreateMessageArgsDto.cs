// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateMessageArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#create-message-jsonform-params
internal sealed record DiscordCreateMessageArgsDto(
	[property: JsonPropertyName(name: "content")]
	string? Content,
	[property: JsonPropertyName(name: "nonce")]
	string? Nonce,
	[property: JsonPropertyName(name: "embeds")]
	DiscordEmbedDto[]? Embeds,
	[property: JsonPropertyName(name: "message_reference")]
	DiscordMessageReferenceDto? MessageReference,
	[property: JsonPropertyName(name: "components")]
	DiscordMessageComponentDto[]? Components,
	[property: JsonPropertyName(name: "attachments")]
	DiscordMessageAttachmentDto[]? Attachments,
	[property: JsonPropertyName(name: "flags")]
	uint? Flags
)
{
	public static DiscordCreateMessageArgsDto FromModel(DiscordCreateMessageArgs model) =>
		new(
			Content: model.Content,
			Nonce: model.Nonce,
			Embeds: model.Embeds?.Select(selector: DiscordEmbedDto.FromModel).ToArray(),
			MessageReference: model.MessageReference != null
				? DiscordMessageReferenceDto.FromModel(model: model.MessageReference)
				: null,
			Components: model.Components?.Select(selector: DiscordMessageComponentDto.FromModel).ToArray(),
			Attachments: model.Attachments?.Select(selector: DiscordMessageAttachmentDto.FromModel).ToArray(),
			Flags: model.Flags != null ? (uint)model.Flags : null
		);
}