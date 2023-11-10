// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackModal.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Models.Interactions;

public record DiscordInteractionCallbackModal : DiscordInteractionCallbackData
{
	public IReadOnlyCollection<DiscordMessageComponent> Components { get; init; } = null!;

	public string CustomId { get; init; } = null!;

	public string Title { get; init; } = null!;
}