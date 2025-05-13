// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageSelectMenuOption.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Emojis;

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

public sealed class DiscordMessageSelectMenuOption()
{
	public bool? Default { get; init; }

	public string? Description { get; init; }

	public DiscordEmoji? Emoji { get; init; }

	public required string Label { get; init; } = null!;

	public required string Value { get; init; } = null!;
}