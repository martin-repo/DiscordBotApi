// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageAttachmentDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#attachment-object-attachment-structure
internal sealed record DiscordMessageAttachmentDto(
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
	public static DiscordMessageAttachmentDto FromModel(DiscordMessageAttachment model) =>
		new(
			Id: model.Id.ToString(),
			Filename: model.Filename,
			Description: model.Description,
			ContentType: model.ContentType,
			Size: model.Size,
			Url: model.Url,
			ProxyUrl: model.ProxyUrl,
			Height: model.Height,
			Width: model.Width,
			Ephemeral: model.Ephemeral
		);

	public DiscordMessageAttachment ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			Filename = Filename,
			Description = Description,
			ContentType = ContentType,
			Size = Size,
			Url = Url,
			ProxyUrl = ProxyUrl,
			Height = Height,
			Width = Width,
			Ephemeral = Ephemeral
		};
}