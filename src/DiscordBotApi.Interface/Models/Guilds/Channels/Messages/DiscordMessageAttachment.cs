// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageAttachment.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public sealed class DiscordMessageAttachment
{
	public string? ContentType { get; init; }

	public string? Description { get; init; }

	public bool? Ephemeral { get; init; }

	public string? Filename { get; init; }

	public int? Height { get; init; }

	public required ulong Id { get; init; }

	public string? ProxyUrl { get; init; }

	public int? Size { get; init; }

	public string? Url { get; init; }

	public int? Width { get; init; }
}