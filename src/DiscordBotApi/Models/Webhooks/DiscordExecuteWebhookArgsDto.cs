// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordExecuteWebhookArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Models.Webhooks;

// https://discord.com/developers/docs/resources/webhook#execute-webhook-jsonform-params
internal record DiscordExecuteWebhookArgsDto(
	[property: JsonPropertyName(name: "content")]
	string? Content,
	[property: JsonPropertyName(name: "embeds")]
	DiscordEmbedDto[]? Embeds,
	[property: JsonPropertyName(name: "components")]
	DiscordMessageComponentDto[]? Components,
	[property: JsonPropertyName(name: "attachments")]
	DiscordMessageAttachmentDto[]? Attachments,
	[property: JsonPropertyName(name: "flags")]
	uint? Flags
)
{
	internal DiscordExecuteWebhookArgsDto(DiscordExecuteWebhookArgs model) : this(
		Content: model.Content,
		Embeds: model.Embeds?.Select(selector: e => new DiscordEmbedDto(model: e))
			.ToArray(),
		Components: model.Components?.Select(selector: DiscordMessageComponent.ConvertToDto)
			.ToArray(),
		Attachments: model.Attachments?.Select(selector: a => new DiscordMessageAttachmentDto(model: a))
			.ToArray(),
		Flags: model.Flags != null
			? (uint)model.Flags
			: null)
	{
	}
}