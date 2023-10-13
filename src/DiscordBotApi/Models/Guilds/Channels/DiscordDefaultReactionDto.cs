// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordDefaultReactionDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels;

internal record DiscordDefaultReactionDto(
	[property: JsonPropertyName(name: "emoji_id")]
	ulong? EmojiId,
	[property: JsonPropertyName(name: "emoji_name")]
	string? EmojiName
)
{
	internal DiscordDefaultReactionDto(DiscordDefaultReaction model) : this(EmojiId: model.EmojiId, EmojiName: model.EmojiName)
	{
	}
}