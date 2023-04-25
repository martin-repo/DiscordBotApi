// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEmbed.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Drawing;

using DiscordBotApi.Utilities;

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

public record DiscordEmbed()
{
	internal DiscordEmbed(DiscordEmbedDto dto) : this()
	{
		Title = dto.Title;
		Description = dto.Description;
		Url = dto.Url;
		Color = ColorUtils.IntToColor(colorInt: dto.Color);
		Footer = dto.Footer != null
			? new DiscordFooter(dto: dto.Footer)
			: null;
		Image = dto.Image != null
			? new DiscordImage(dto: dto.Image)
			: null;
		Thumbnail = dto.Thumbnail != null
			? new DiscordThumbnail(dto: dto.Thumbnail)
			: null;
		Video = dto.Video != null
			? new DiscordVideo(dto: dto.Video)
			: null;
		Author = dto.Author != null
			? new DiscordAuthor(dto: dto.Author)
			: null;
		Fields = dto.Fields?.Select(selector: f => new DiscordField(dto: f))
			.ToArray();
	}

	public DiscordAuthor? Author { get; init; }

	public Color? Color { get; init; }

	public string? Description { get; init; }

	public IReadOnlyCollection<DiscordField>? Fields { get; init; }

	public DiscordFooter? Footer { get; init; }

	public DiscordImage? Image { get; init; }

	public DiscordThumbnail? Thumbnail { get; init; }

	public string? Title { get; init; }

	public string? Url { get; init; }

	public DiscordVideo? Video { get; init; }
}