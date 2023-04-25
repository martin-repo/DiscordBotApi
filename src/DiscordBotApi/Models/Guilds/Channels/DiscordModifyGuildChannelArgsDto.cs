// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyGuildChannelArgsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels;

internal record DiscordModifyGuildChannelArgsDto(
	[property: JsonPropertyName(name: "name")]
	string? Name,
	[property: JsonPropertyName(name: "type")]
	int? Type,
	[property: JsonPropertyName(name: "position")]
	int? Position,
	[property: JsonPropertyName(name: "topic")]
	string? Topic,
	[property: JsonPropertyName(name: "permission_overwrites")]
	DiscordPermissionOverwriteDto[]? PermissionOverwrites,
	[property: JsonPropertyName(name: "parent_id")]
	string? ParentId
)
{
	internal DiscordModifyGuildChannelArgsDto(DiscordModifyGuildChannelArgs model) : this(
		Name: model.Name,
		Type: model.Type != null
			? (int)model.Type
			: null,
		Position: model.Position,
		Topic: model.Topic,
		PermissionOverwrites: model.PermissionOverwrites?.Select(selector: po => new DiscordPermissionOverwriteDto(model: po))
			.ToArray(),
		ParentId: model.ParentId != null
			? model.ParentId.ToString()
			: null)
	{
	}
}