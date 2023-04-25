// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildChannelArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Channels;

namespace DiscordBotApi.Models.Guilds;

public record DiscordCreateGuildChannelArgs
{
	public string Name { get; init; } = "";

	public ulong? ParentId { get; init; }

	public IReadOnlyCollection<DiscordPermissionOverwrite>? PermissionOverwrites { get; init; }

	public string? Topic { get; init; }

	public DiscordChannelType? Type { get; init; }
}