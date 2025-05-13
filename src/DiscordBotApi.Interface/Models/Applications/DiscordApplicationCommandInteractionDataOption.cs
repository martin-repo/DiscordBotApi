// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandInteractionDataOption.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Applications;

public sealed class DiscordApplicationCommandInteractionDataOption
{
	public bool? Focused { get; init; }

	public required string Name { get; init; }

	public IReadOnlyCollection<DiscordApplicationCommandInteractionDataOption>? Options { get; init; }

	public required DiscordApplicationCommandOptionType Type { get; init; }

	public object? Value { get; init; }
}