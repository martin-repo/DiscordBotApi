// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordStartThreadWithMessageArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels;

public record DiscordStartThreadWithMessageArgs
{
	public AutoArchiveDuration? AutoArchiveDuration { get; init; }

	public string Name { get; init; } = "";
}