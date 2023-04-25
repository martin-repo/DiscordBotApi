// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordChannelDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels;

internal record DiscordChannelDto(
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
	DiscordThreadMetadataDto? ThreadMetadata
);