// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyGuildChannelArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#modify-channel-json-params-guild-channel
public sealed class DiscordModifyGuildChannelArgs
{
	public IReadOnlyCollection<DiscordForumTag>? AvailableTags { get; init; }

	public int? DefaultAutoArchiveDuration { get; init; }

	public DiscordForumLayoutType? DefaultForumLayout { get; init; }

	public DiscordDefaultReaction? DefaultReactionEmoji { get; init; }

	public DiscordSortOrderType? DefaultSortOrder { get; init; }

	public string? Name { get; init; }

	public ulong? ParentId { get; init; }

	public IReadOnlyCollection<DiscordPermissionOverwrite>? PermissionOverwrites { get; init; }

	public int? Position { get; init; }

	public string? Topic { get; init; }

	public DiscordChannelType? Type { get; init; }
}