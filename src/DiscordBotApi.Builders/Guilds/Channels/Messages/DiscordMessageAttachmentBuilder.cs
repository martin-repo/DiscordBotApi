// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageAttachmentBuilder.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages;

// WARNING! This file was generated by a tool.
//          Any changes made to this file will be lost if the code is regenerated.
public class DiscordMessageAttachmentBuilder
{
	private string? _contentType;
	private string? _description;
	private bool? _ephemeral;
	private string? _filename;
	private int? _height;
	private ulong _id = default!;
	private string? _proxyUrl;
	private int? _size;
	private string? _url;
	private int? _width;

	public DiscordMessageAttachmentBuilder WithContentType(string? contentType)
	{
		_contentType = contentType;
		return this;
	}

	public DiscordMessageAttachmentBuilder WithDescription(string? description)
	{
		_description = description;
		return this;
	}

	public DiscordMessageAttachmentBuilder WithEphemeral(bool? ephemeral)
	{
		_ephemeral = ephemeral;
		return this;
	}

	public DiscordMessageAttachmentBuilder WithFilename(string? filename)
	{
		_filename = filename;
		return this;
	}

	public DiscordMessageAttachmentBuilder WithHeight(int? height)
	{
		_height = height;
		return this;
	}

	public DiscordMessageAttachmentBuilder WithId(ulong id)
	{
		_id = id;
		return this;
	}

	public DiscordMessageAttachmentBuilder WithProxyUrl(string? proxyUrl)
	{
		_proxyUrl = proxyUrl;
		return this;
	}

	public DiscordMessageAttachmentBuilder WithSize(int? size)
	{
		_size = size;
		return this;
	}

	public DiscordMessageAttachmentBuilder WithUrl(string? url)
	{
		_url = url;
		return this;
	}

	public DiscordMessageAttachmentBuilder WithWidth(int? width)
	{
		_width = width;
		return this;
	}

	public DiscordMessageAttachment Build() =>
		new()
		{
			ContentType = _contentType,
			Description = _description,
			Ephemeral = _ephemeral,
			Filename = _filename,
			Height = _height,
			Id = _id,
			ProxyUrl = _proxyUrl,
			Size = _size,
			Url = _url,
			Width = _width,
		};
}