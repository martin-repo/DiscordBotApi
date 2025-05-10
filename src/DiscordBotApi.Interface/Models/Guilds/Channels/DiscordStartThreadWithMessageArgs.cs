// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordStartThreadWithMessageArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

public sealed class DiscordStartThreadWithMessageArgs
{
	public int? AutoArchiveDuration { get; init; }

	public required string Name { get; init; }
}