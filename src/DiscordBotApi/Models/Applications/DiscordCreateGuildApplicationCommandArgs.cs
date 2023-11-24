// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildApplicationCommandArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds;

namespace DiscordBotApi.Models.Applications;

public record DiscordCreateGuildApplicationCommandArgs
{
	public DiscordPermissions? DefaultMemberPermissions { get; init; }

	public string Description { get; init; } = "";

	public string Name { get; init; } = "";

	public IReadOnlyCollection<DiscordApplicationCommandOption>? Options { get; init; }

	public DiscordApplicationCommandType? Type { get; init; }
}