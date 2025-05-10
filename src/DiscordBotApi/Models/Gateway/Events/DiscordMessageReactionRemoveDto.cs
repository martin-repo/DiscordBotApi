// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReactionRemoveDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Events;
using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway#message-reaction-remove-message-reaction-remove-event-fields
internal sealed record DiscordMessageReactionRemoveDto(
	[property: JsonPropertyName(name: "user_id")]
	string UserId,
	[property: JsonPropertyName(name: "channel_id")]
	string ChannelId,
	[property: JsonPropertyName(name: "message_id")]
	string MessageId,
	[property: JsonPropertyName(name: "guild_id")]
	string? GuildId,
	[property: JsonPropertyName(name: "emoji")]
	DiscordEmojiDto Emoji
)
{
	public DiscordMessageReactionRemove ToModel() =>
		new()
		{
			UserId = ulong.Parse(s: UserId),
			ChannelId = ulong.Parse(s: ChannelId),
			MessageId = ulong.Parse(s: MessageId),
			GuildId = GuildId != null ? ulong.Parse(s: GuildId) : null,
			Emoji = Emoji.ToModel()
		};
}