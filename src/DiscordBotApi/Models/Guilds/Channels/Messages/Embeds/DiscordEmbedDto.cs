// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEmbedDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;
using DiscordBotApi.Utilities;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

// https://discord.com/developers/docs/developer-tools/embedded-app-sdk#embed
internal sealed record DiscordEmbedDto(
	[property: JsonPropertyName(name: "title")]
	string? Title,
	[property: JsonPropertyName(name: "description")]
	string? Description,
	[property: JsonPropertyName(name: "url")]
	string? Url,
	[property: JsonPropertyName(name: "timestamp")]
	string? Timestamp,
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
	public static DiscordEmbedDto FromModel(DiscordEmbed model) =>
		new(
			Title: model.Title,
			Description: model.Description,
			Url: model.Url,
			Timestamp: model.Timestamp?.ToString(format: "O"),
			Color: ColorUtils.ColorToInt(color: model.Color),
			Footer: model.Footer != null ? DiscordFooterDto.FromModel(model: model.Footer) : null,
			Image: model.Image != null ? DiscordImageDto.FromModel(model: model.Image) : null,
			Thumbnail: model.Thumbnail != null ? DiscordThumbnailDto.FromModel(model: model.Thumbnail) : null,
			Video: model.Video != null ? DiscordVideoDto.FromModel(model: model.Video) : null,
			Author: model.Author != null ? DiscordAuthorDto.FromModel(model: model.Author) : null,
			Fields: model.Fields?.Select(selector: DiscordFieldDto.FromModel).ToArray()
		);

	public DiscordEmbed ToModel() =>
		new()
		{
			Title = Title,
			Description = Description,
			Url = Url,
			Timestamp = Timestamp != null ? DateTimeOffset.Parse(input: Timestamp) : null,
			Color = ColorUtils.IntToColor(colorInt: Color),
			Footer = Footer?.ToModel(),
			Image = Image?.ToModel(),
			Thumbnail = Thumbnail?.ToModel(),
			Video = Video?.ToModel(),
			Author = Author?.ToModel(),
			Fields = Fields?.Select(selector: f => f.ToModel()).ToArray()
		};
}