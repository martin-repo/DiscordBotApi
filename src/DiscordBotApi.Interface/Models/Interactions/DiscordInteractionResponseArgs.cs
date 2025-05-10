// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionResponseArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Interactions;

public sealed class DiscordInteractionResponseArgs
{
	public DiscordInteractionCallbackData? Data { get; init; }

	public required DiscordInteractionCallbackType Type { get; init; }
}