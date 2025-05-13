// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageActionRow.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

public sealed class DiscordMessageActionRow : DiscordMessageComponent
{
	public required IReadOnlyCollection<DiscordMessageComponent> Components { get; init; }
}