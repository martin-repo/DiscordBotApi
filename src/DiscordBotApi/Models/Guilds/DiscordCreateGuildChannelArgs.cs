// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildChannelArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Channels;

namespace DiscordBotApi.Models.Guilds;

// https://discord.com/developers/docs/resources/guild#create-guild-channel
public record DiscordCreateGuildChannelArgs
{
	public IReadOnlyCollection<DiscordForumTag>? AvailableTags { get; init; }

	public int? DefaultAutoArchiveDuration { get; init; }

	public DiscordForumLayoutType? DefaultForumLayout { get; init; }

	public DiscordDefaultReaction? DefaultReactionEmoji { get; init; }

	public DiscordSortOrderType? DefaultSortOrder { get; init; }

	public string Name { get; init; } = "";

	public ulong? ParentId { get; init; }

	public IReadOnlyCollection<DiscordPermissionOverwrite>? PermissionOverwrites { get; init; }

	public int? Position { get; init; }

	public string? Topic { get; init; }

	public DiscordChannelType? Type { get; init; }
}