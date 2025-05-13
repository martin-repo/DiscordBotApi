// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordChannel.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#channel-object
public sealed class DiscordChannel
{
	public IReadOnlyCollection<DiscordForumTag>? AvailableTags { get; init; }

	public int? DefaultAutoArchiveDuration { get; init; }

	public DiscordForumLayoutType? DefaultForumLayout { get; init; }

	public DiscordDefaultReaction? DefaultReactionEmoji { get; init; }

	public DiscordSortOrderType? DefaultSortOrder { get; init; }

	public ulong? GuildId { get; init; }

	public required ulong Id { get; init; }

	public string? Name { get; init; }

	public ulong? ParentId { get; init; }

	public IReadOnlyCollection<DiscordPermissionOverwrite>? PermissionOverwrites { get; init; }

	public int? Position { get; init; }

	public DiscordThreadMetadata? ThreadMetadata { get; init; }

	public string? Topic { get; init; }

	public required DiscordChannelType Type { get; init; }
}