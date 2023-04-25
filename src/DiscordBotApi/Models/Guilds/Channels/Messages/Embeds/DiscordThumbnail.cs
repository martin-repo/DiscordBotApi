// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThumbnail.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

public record DiscordThumbnail()
{
	internal DiscordThumbnail(DiscordThumbnailDto dto) : this()
	{
		Url = dto.Url;
	}

	public string Url { get; init; } = "";
}