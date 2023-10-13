// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordForumTagDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels;

internal record DiscordForumTagDto(
	[property: JsonPropertyName(name: "id")]
	string? Id,
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "moderated")]
	bool? Moderated,
	[property: JsonPropertyName(name: "emoji_id")]
	string? EmojiId,
	[property: JsonPropertyName(name: "emoji_name")]
	string? EmojiName
)
{
	internal DiscordForumTagDto(DiscordForumTag model) : this(
		Id: model.Id?.ToString(),
		Name: model.Name,
		Moderated: model.Moderated,
		EmojiId: model.EmojiId?.ToString(),
		EmojiName: model.EmojiName)
	{
	}
}