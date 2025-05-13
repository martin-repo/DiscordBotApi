// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageSelectMenu.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

public sealed class DiscordMessageSelectMenu() : DiscordMessageComponent
{
	public required string CustomId { get; init; }

	public bool? Disabled { get; init; }

	public int? MaxValues { get; init; }

	public int? MinValues { get; init; }

	public IReadOnlyCollection<DiscordMessageSelectMenuOption>? Options { get; init; }

	public string? Placeholder { get; init; }
}