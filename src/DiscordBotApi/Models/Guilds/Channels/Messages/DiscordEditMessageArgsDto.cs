// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEditMessageArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

internal sealed record DiscordEditMessageArgsDto(
	[property: JsonPropertyName(name: "content")]
	string? Content,
	[property: JsonPropertyName(name: "embeds")]
	DiscordEmbedDto[]? Embeds,
	[property: JsonPropertyName(name: "components")]
	DiscordMessageComponentDto[]? Components,
	[property: JsonPropertyName(name: "attachments")]
	DiscordMessageAttachmentDto[]? Attachments
)
{
	public static DiscordEditMessageArgsDto FromModel(DiscordEditMessageArgs model) =>
		new(
			Content: model.Content,
			Embeds: model.Embeds?.Select(selector: DiscordEmbedDto.FromModel).ToArray(),
			Components: model.Components?.Select(selector: DiscordMessageComponentDto.FromModel).ToArray(),
			Attachments: model.Attachments?.Select(selector: DiscordMessageAttachmentDto.FromModel).ToArray()
		);
}