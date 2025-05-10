// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordForumMessageArgsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#start-thread-in-forum-channel-forum-thread-message-params-object
internal sealed record DiscordForumMessageArgsDto(
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
	public static DiscordForumMessageArgsDto FromModel(DiscordForumMessageArgs model) =>
		new(
			Content: model.Content,
			Embeds: model.Embeds?.Select(selector: DiscordEmbedDto.FromModel).ToArray(),
			Components: model.Components?.Select(selector: c => DiscordMessageComponentDto.FromModel(model: c)).ToArray(),
			Attachments: model.Attachments?.Select(selector: DiscordMessageAttachmentDto.FromModel).ToArray()
		);
}