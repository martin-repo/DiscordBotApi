// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordAuthor.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;

public sealed class DiscordAuthor
{
	public string? IconUrl { get; init; }

	public required string Name { get; init; }

	public string? Url { get; init; }
}