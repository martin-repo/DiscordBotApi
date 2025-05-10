// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordFooter.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;

public sealed class DiscordFooter
{
	public string? IconUrl { get; init; }

	public required string Text { get; init; }
}