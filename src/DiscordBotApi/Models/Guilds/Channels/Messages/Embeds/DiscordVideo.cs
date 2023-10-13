// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordVideo.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

public record DiscordVideo()
{
	internal DiscordVideo(DiscordVideoDto dto) : this()
	{
		Url = dto.Url;
	}

	public string? Url { get; init; }
}