// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyGuildChannelArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels;

public record DiscordModifyGuildChannelArgs
{
	public string? Name { get; init; }

	public ulong? ParentId { get; init; }

	public IReadOnlyCollection<DiscordPermissionOverwrite>? PermissionOverwrites { get; init; }

	public int? Position { get; init; }

	public string? Topic { get; init; }

	public DiscordChannelType? Type { get; init; }
}