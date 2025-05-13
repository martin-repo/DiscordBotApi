// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordDefaultReactionDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels;

namespace DiscordBotApi.Models.Guilds.Channels;

internal sealed record DiscordDefaultReactionDto(
	[property: JsonPropertyName(name: "emoji_id")]
	ulong? EmojiId,
	[property: JsonPropertyName(name: "emoji_name")]
	string? EmojiName
)
{
	public static DiscordDefaultReactionDto FromModel(DiscordDefaultReaction model) =>
		new(EmojiId: model.EmojiId, EmojiName: model.EmojiName);

	public DiscordDefaultReaction ToModel() =>
		new()
		{
			EmojiId = EmojiId,
			EmojiName = EmojiName
		};
}