// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildChannelArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Channels;

namespace DiscordBotApi.Interface.Models.Guilds;

// https://discord.com/developers/docs/resources/guild#create-guild-channel
public sealed class DiscordCreateGuildChannelArgs
{
	public IReadOnlyCollection<DiscordForumTag>? AvailableTags { get; init; }

	public int? DefaultAutoArchiveDuration { get; init; }

	public DiscordForumLayoutType? DefaultForumLayout { get; init; }

	public DiscordDefaultReaction? DefaultReactionEmoji { get; init; }

	public DiscordSortOrderType? DefaultSortOrder { get; init; }

	public required string Name { get; init; }

	public ulong? ParentId { get; init; }

	public IReadOnlyCollection<DiscordPermissionOverwrite>? PermissionOverwrites { get; init; }

	public int? Position { get; init; }

	public string? Topic { get; init; }

	public DiscordChannelType? Type { get; init; }
}