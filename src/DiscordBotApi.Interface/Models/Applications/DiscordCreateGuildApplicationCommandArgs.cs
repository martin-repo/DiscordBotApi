// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildApplicationCommandArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds;

namespace DiscordBotApi.Interface.Models.Applications;

public sealed class DiscordCreateGuildApplicationCommandArgs
{
	public DiscordPermissions? DefaultMemberPermissions { get; init; }

	public required string Description { get; init; }

	public required string Name { get; init; }

	public IReadOnlyCollection<DiscordApplicationCommandOption>? Options { get; init; }

	public DiscordApplicationCommandType? Type { get; init; }
}