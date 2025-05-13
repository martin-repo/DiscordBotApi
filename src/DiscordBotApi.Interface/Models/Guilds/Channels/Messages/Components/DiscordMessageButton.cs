// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageButton.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Emojis;

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

public sealed class DiscordMessageButton : DiscordMessageComponent
{
	public string? CustomId { get; init; }

	public bool? Disabled { get; init; }

	public DiscordEmoji? Emoji { get; init; }

	public string? Label { get; init; }

	public required DiscordMessageButtonStyle Style { get; init; }

	public string? Url { get; init; }
}