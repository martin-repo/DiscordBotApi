// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGetGuildArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds;

public record DiscordGetGuildArgs
{
	public bool? WithCounts { get; init; }
}