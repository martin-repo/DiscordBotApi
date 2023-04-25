// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEmbedDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Utilities;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

// https://discord.com/developers/docs/resources/channel#embed-object-embed-structure
internal record DiscordEmbedDto(
	[property: JsonPropertyName(name: "title")]
	string? Title,
	[property: JsonPropertyName(name: "description")]
	string? Description,
	[property: JsonPropertyName(name: "url")]
	string? Url,
	[property: JsonPropertyName(name: "color")]
	int? Color,
	[property: JsonPropertyName(name: "footer")]
	DiscordFooterDto? Footer,
	[property: JsonPropertyName(name: "image")]
	DiscordImageDto? Image,
	[property: JsonPropertyName(name: "thumbnail")]
	DiscordThumbnailDto? Thumbnail,
	[property: JsonPropertyName(name: "video")]
	DiscordVideoDto? Video,
	[property: JsonPropertyName(name: "author")]
	DiscordAuthorDto? Author,
	[property: JsonPropertyName(name: "fields")]
	DiscordFieldDto[]? Fields
)
{
	internal DiscordEmbedDto(DiscordEmbed model) : this(
		Title: model.Title,
		Description: model.Description,
		Url: model.Url,
		Color: ColorUtils.ColorToInt(color: model.Color),
		Footer: model.Footer != null
			? new DiscordFooterDto(model: model.Footer)
			: null,
		Image: model.Image != null
			? new DiscordImageDto(model: model.Image)
			: null,
		Thumbnail: model.Thumbnail != null
			? new DiscordThumbnailDto(model: model.Thumbnail)
			: null,
		Video: model.Video != null
			? new DiscordVideoDto(model: model.Video)
			: null,
		Author: model.Author != null
			? new DiscordAuthorDto(model: model.Author)
			: null,
		Fields: model.Fields?.Select(selector: f => new DiscordFieldDto(model: f))
			.ToArray())
	{
	}
}