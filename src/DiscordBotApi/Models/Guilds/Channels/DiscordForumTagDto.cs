// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordForumTagDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels;

namespace DiscordBotApi.Models.Guilds.Channels;

internal sealed record DiscordForumTagDto(
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
	public static DiscordForumTagDto FromModel(DiscordForumTag model) =>
		new(
			Id: model.Id?.ToString(),
			Name: model.Name,
			Moderated: model.Moderated,
			EmojiId: model.EmojiId?.ToString(),
			EmojiName: model.EmojiName
		);

	public DiscordForumTag ToModel() =>
		new()
		{
			Id = Id != null ? ulong.Parse(s: Id) : null,
			Name = Name,
			Moderated = Moderated,
			EmojiId = EmojiId != null ? ulong.Parse(s: EmojiId) : null,
			EmojiName = EmojiName
		};
}