// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEmbedBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages.Embeds;

public class DiscordEmbedBuilder
{
	private DiscordAuthor? _author;
	private Color? _color;
	private string? _description;
	private List<DiscordField>? _fields;
	private DiscordFooter? _footer;
	private DiscordImage? _image;
	private DiscordThumbnail? _thumbnail;
	private string? _title;
	private string? _url;
	private DiscordVideo? _video;

	public DiscordEmbedBuilder WithAuthor(DiscordAuthor? author)
	{
		_author = author;
		return this;
	}

	public DiscordEmbedBuilder WithColor(Color? color)
	{
		_color = color;
		return this;
	}

	public DiscordEmbedBuilder WithDescription(string? description)
	{
		_description = description;
		return this;
	}

	public DiscordEmbedBuilder AddField(Action<DiscordFieldBuilder> builderAction)
	{
		var builder = new DiscordFieldBuilder();
		builderAction(obj: builder);
		_fields ??= new List<DiscordField>();
		_fields.Add(item: builder.Build());
		return this;
	}

	public DiscordEmbedBuilder AddField(DiscordField item)
	{
		_fields ??= new List<DiscordField>();
		_fields.Add(item: item);
		return this;
	}

	public DiscordEmbedBuilder WithFooter(DiscordFooter? footer)
	{
		_footer = footer;
		return this;
	}

	public DiscordEmbedBuilder WithImage(DiscordImage? image)
	{
		_image = image;
		return this;
	}

	public DiscordEmbedBuilder WithThumbnail(DiscordThumbnail? thumbnail)
	{
		_thumbnail = thumbnail;
		return this;
	}

	public DiscordEmbedBuilder WithTitle(string? title)
	{
		_title = title;
		return this;
	}

	public DiscordEmbedBuilder WithUrl(string? url)
	{
		_url = url;
		return this;
	}

	public DiscordEmbedBuilder WithVideo(DiscordVideo? video)
	{
		_video = video;
		return this;
	}

	public DiscordEmbed Build() =>
		new()
		{
			Author = _author,
			Color = _color,
			Description = _description,
			Fields = _fields?.ToImmutableArray(),
			Footer = _footer,
			Image = _image,
			Thumbnail = _thumbnail,
			Title = _title,
			Url = _url,
			Video = _video,
		};
}