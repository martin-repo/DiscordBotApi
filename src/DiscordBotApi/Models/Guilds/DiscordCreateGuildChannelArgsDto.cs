// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildChannelArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds.Channels;

namespace DiscordBotApi.Models.Guilds;

internal record DiscordCreateGuildChannelArgsDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "type")]
	int? Type,
	[property: JsonPropertyName(name: "topic")]
	string? Topic,
	[property: JsonPropertyName(name: "position")]
	int? Position,
	[property: JsonPropertyName(name: "permission_overwrites")]
	DiscordPermissionOverwriteDto[]? PermissionOverwrites,
	[property: JsonPropertyName(name: "parent_id")]
	string? ParentId,
	[property: JsonPropertyName(name: "default_auto_archive_duration")]
	int? DefaultAutoArchiveDuration,
	[property: JsonPropertyName(name: "default_reaction_emoji")]
	DiscordDefaultReactionDto? DefaultReactionEmoji,
	[property: JsonPropertyName(name: "available_tags")]
	DiscordForumTagDto[]? AvailableTags,
	[property: JsonPropertyName(name: "default_sort_order")]
	int? DefaultSortOrder,
	[property: JsonPropertyName(name: "default_forum_layout")]
	int? DefaultForumLayout
)
{
	internal DiscordCreateGuildChannelArgsDto(DiscordCreateGuildChannelArgs model) : this(
		Name: model.Name,
		Type: model.Type != null
			? (int)model.Type
			: null,
		Topic: model.Topic,
		Position: model.Position,
		PermissionOverwrites: model.PermissionOverwrites?.Select(selector: po => new DiscordPermissionOverwriteDto(model: po))
			.ToArray(),
		ParentId: model.ParentId is not null
			? model.ParentId.ToString()
			: null,
		DefaultAutoArchiveDuration: model.DefaultAutoArchiveDuration,
		DefaultReactionEmoji: model.DefaultReactionEmoji is not null
			? new DiscordDefaultReactionDto(model: model.DefaultReactionEmoji)
			: null,
		AvailableTags: model.AvailableTags?.Select(selector: at => new DiscordForumTagDto(model: at))
			.ToArray(),
		DefaultSortOrder: model.DefaultSortOrder != null
			? (int)model.DefaultSortOrder
			: null,
		DefaultForumLayout: model.DefaultForumLayout != null
			? (int)model.DefaultForumLayout
			: null)
	{
	}
}