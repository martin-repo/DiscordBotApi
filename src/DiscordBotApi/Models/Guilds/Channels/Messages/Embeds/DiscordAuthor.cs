// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordAuthor.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

public record DiscordAuthor()
{
	internal DiscordAuthor(DiscordAuthorDto dto) : this()
	{
		Name = dto.Name;
		IconUrl = dto.IconUrl;
	}

	public string? IconUrl { get; init; }

	public string Name { get; init; } = "";
}