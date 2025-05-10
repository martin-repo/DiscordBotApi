// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReactionDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

internal sealed record DiscordReactionDto(
	[property: JsonPropertyName(name: "count")]
	int Count,
	[property: JsonPropertyName(name: "me")]
	bool Me,
	[property: JsonPropertyName(name: "emoji")]
	DiscordEmojiDto Emoji
)
{
	public DiscordReaction ToModel() =>
		new()
		{
			Count = Count,
			Me = Me,
			Emoji = Emoji.ToModel()
		};
}