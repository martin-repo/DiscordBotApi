// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEmbed.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Drawing;

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;

public sealed class DiscordEmbed
{
	public DiscordAuthor? Author { get; init; }

	public Color? Color { get; init; }

	public string? Description { get; init; }

	public IReadOnlyCollection<DiscordField>? Fields { get; init; }

	public DiscordFooter? Footer { get; init; }

	public DiscordImage? Image { get; init; }

	public DiscordThumbnail? Thumbnail { get; init; }

	public DateTimeOffset? Timestamp { get; init; }

	public string? Title { get; init; }

	public string? Url { get; init; }

	public DiscordVideo? Video { get; init; }
}