// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOption.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Channels;

namespace DiscordBotApi.Interface.Models.Applications;

public sealed class DiscordApplicationCommandOption
{
	public bool? Autocomplete { get; init; }

	public IReadOnlyCollection<DiscordChannelType>? ChannelTypes { get; init; }

	public IReadOnlyCollection<DiscordApplicationCommandOptionChoice>? Choices { get; init; }

	public required string Description { get; init; }

	public int? MaxLength { get; init; }

	public object? MaxValue { get; init; }

	public int? MinLength { get; init; }

	public object? MinValue { get; init; }

	public required string Name { get; init; }

	public IReadOnlyCollection<DiscordApplicationCommandOption>? Options { get; init; }

	public bool? Required { get; init; }

	public required DiscordApplicationCommandOptionType Type { get; init; }
}