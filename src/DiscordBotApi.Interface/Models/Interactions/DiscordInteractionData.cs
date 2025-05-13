// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionData.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Applications;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Interface.Models.Interactions;

public sealed class DiscordInteractionData
{
	public IReadOnlyCollection<DiscordMessageComponent>? Components { get; init; }

	public DiscordMessageComponentType? ComponentType { get; init; }

	public string? CustomId { get; init; }

	public ulong? Id { get; init; }

	public string? Name { get; init; }

	public IReadOnlyCollection<DiscordApplicationCommandInteractionDataOption>? Options { get; init; }

	public DiscordApplicationCommandType? Type { get; init; }

	public IReadOnlyCollection<string>? Values { get; init; }
}