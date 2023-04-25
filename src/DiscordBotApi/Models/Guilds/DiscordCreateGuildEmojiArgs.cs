// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildEmojiArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds;

public record DiscordCreateGuildEmojiArgs
{
	public string FilePath { get; init; } = "";

	public string Name { get; init; } = "";
}