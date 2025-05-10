// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordChannelDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels;

namespace DiscordBotApi.Models.Guilds.Channels;

internal sealed record DiscordChannelDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "type")]
	int Type,
	[property: JsonPropertyName(name: "guild_id")]
	string? GuildId,
	[property: JsonPropertyName(name: "position")]
	int? Position,
	[property: JsonPropertyName(name: "permission_overwrites")]
	DiscordPermissionOverwriteDto[]? PermissionOverwrites,
	[property: JsonPropertyName(name: "name")]
	string? Name,
	[property: JsonPropertyName(name: "topic")]
	string? Topic,
	[property: JsonPropertyName(name: "parent_id")]
	string? ParentId,
	[property: JsonPropertyName(name: "thread_metadata")]
	DiscordThreadMetadataDto? ThreadMetadata,
	[property: JsonPropertyName(name: "available_tags")]
	DiscordForumTagDto[]? AvailableTags,
	[property: JsonPropertyName(name: "default_auto_archive_duration")]
	int? DefaultAutoArchiveDuration,
	[property: JsonPropertyName(name: "default_reaction_emoji")]
	DiscordDefaultReactionDto? DefaultReactionEmoji,
	[property: JsonPropertyName(name: "default_sort_order")]
	int? DefaultSortOrder,
	[property: JsonPropertyName(name: "default_forum_layout")]
	int? DefaultForumLayout
)
{
	public DiscordChannel ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			Type = (DiscordChannelType)Type,
			GuildId = GuildId != null ? ulong.Parse(s: GuildId) : null,
			Position = Position,
			PermissionOverwrites = PermissionOverwrites?.Select(selector: o => o.ToModel()).ToArray(),
			Name = Name,
			Topic = Topic,
			ParentId = ParentId != null ? ulong.Parse(s: ParentId) : null,
			ThreadMetadata = ThreadMetadata?.ToModel(),
			AvailableTags = AvailableTags?.Select(selector: t => t.ToModel()).ToArray(),
			DefaultAutoArchiveDuration = DefaultAutoArchiveDuration,
			DefaultReactionEmoji = DefaultReactionEmoji?.ToModel(),
			DefaultSortOrder = DefaultSortOrder != null ? (DiscordSortOrderType)DefaultSortOrder : null,
			DefaultForumLayout = DefaultForumLayout != null ? (DiscordForumLayoutType)DefaultForumLayout : null
		};
}