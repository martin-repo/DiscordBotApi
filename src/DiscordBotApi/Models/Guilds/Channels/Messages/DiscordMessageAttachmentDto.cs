// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageAttachmentDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#attachment-object-attachment-structure
internal record DiscordMessageAttachmentDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "filename")]
	string? Filename,
	[property: JsonPropertyName(name: "description")]
	string? Description,
	[property: JsonPropertyName(name: "content_type")]
	string? ContentType,
	[property: JsonPropertyName(name: "size")]
	int? Size,
	[property: JsonPropertyName(name: "url")]
	string? Url,
	[property: JsonPropertyName(name: "proxy_url")]
	string? ProxyUrl,
	[property: JsonPropertyName(name: "height")]
	int? Height,
	[property: JsonPropertyName(name: "width")]
	int? Width,
	[property: JsonPropertyName(name: "ephemeral")]
	bool? Ephemeral
)

{
	internal DiscordMessageAttachmentDto(DiscordMessageAttachment model) : this(
		Id: model.Id.ToString(),
		Filename: model.Filename,
		Description: model.Description,
		ContentType: model.ContentType,
		Size: model.Size,
		Url: model.Url,
		ProxyUrl: model.ProxyUrl,
		Height: model.Height,
		Width: model.Width,
		Ephemeral: model.Ephemeral)
	{
	}
}