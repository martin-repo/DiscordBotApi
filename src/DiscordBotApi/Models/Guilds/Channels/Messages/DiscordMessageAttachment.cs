// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageAttachment.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

public record DiscordMessageAttachment()
{
	internal DiscordMessageAttachment(DiscordMessageAttachmentDto dto) : this()
	{
		Id = ulong.Parse(s: dto.Id);
		Filename = dto.Filename;
		Description = dto.Description;
		ContentType = dto.ContentType;
		Size = dto.Size;
		Url = dto.Url;
		ProxyUrl = dto.ProxyUrl;
		Height = dto.Height;
		Width = dto.Width;
		Ephemeral = dto.Ephemeral;
	}

	public string? ContentType { get; init; }

	public string? Description { get; init; }

	public bool? Ephemeral { get; init; }

	public string? Filename { get; init; }

	public int? Height { get; init; }

	public ulong Id { get; init; }

	public string? ProxyUrl { get; init; }

	public int? Size { get; init; }

	public string? Url { get; init; }

	public int? Width { get; init; }
}