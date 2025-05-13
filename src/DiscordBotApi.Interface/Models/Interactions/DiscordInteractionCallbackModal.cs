// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackModal.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Interface.Models.Interactions;

public sealed class DiscordInteractionCallbackModal : DiscordInteractionCallbackData
{
	public required IReadOnlyCollection<DiscordMessageComponent> Components { get; init; } = null!;

	public required string CustomId { get; init; } = null!;

	public required string Title { get; init; } = null!;
}