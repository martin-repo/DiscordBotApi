// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildEmojiArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds;

public sealed class DiscordCreateGuildEmojiArgs
{
	public required string FilePath { get; init; }

	public required string Name { get; init; }
}