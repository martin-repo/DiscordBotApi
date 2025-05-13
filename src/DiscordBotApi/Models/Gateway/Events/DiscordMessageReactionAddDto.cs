// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReactionAddDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Events;
using DiscordBotApi.Models.Guilds;
using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/topics/gateway-events#message-reaction-add-message-reaction-add-event-fields
internal sealed record DiscordMessageReactionAddDto(
	[property: JsonPropertyName(name: "user_id")]
	string UserId,
	[property: JsonPropertyName(name: "channel_id")]
	string ChannelId,
	[property: JsonPropertyName(name: "message_id")]
	string MessageId,
	[property: JsonPropertyName(name: "guild_id")]
	string? GuildId,
	[property: JsonPropertyName(name: "member")]
	DiscordGuildMemberDto? Member,
	[property: JsonPropertyName(name: "emoji")]
	DiscordEmojiDto Emoji,
	[property: JsonPropertyName(name: "message_author_id")]
	string? MessageAuthorId
)
{
	public DiscordMessageReactionAdd ToModel() =>
		new()
		{
			UserId = ulong.Parse(s: UserId),
			ChannelId = ulong.Parse(s: ChannelId),
			MessageId = ulong.Parse(s: MessageId),
			GuildId = GuildId is not null ? ulong.Parse(s: GuildId) : null,
			Member = Member?.ToModel(),
			Emoji = Emoji.ToModel(),
			MessageAuthorId = MessageAuthorId != null ? ulong.Parse(s: MessageAuthorId) : null
		};
}