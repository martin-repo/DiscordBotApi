// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReferenceDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#message-reference-object
internal sealed record DiscordMessageReferenceDto(
	[property: JsonPropertyName(name: "message_id")]
	string? MessageId,
	[property: JsonPropertyName(name: "channel_id")]
	string? ChannelId,
	[property: JsonPropertyName(name: "guild_id")]
	string? GuildId,
	[property: JsonPropertyName(name: "fail_if_not_exists")]
	bool? FailIfNotExists
)
{
	public static DiscordMessageReferenceDto FromModel(DiscordMessageReference model) =>
		new(
			MessageId: model.MessageId?.ToString(),
			ChannelId: model.ChannelId?.ToString(),
			GuildId: model.GuildId?.ToString(),
			FailIfNotExists: model.FailIfNotExists
		);

	public DiscordMessageReference ToModel() =>
		new()
		{
			MessageId = MessageId != null ? ulong.Parse(s: MessageId) : null,
			ChannelId = ChannelId != null ? ulong.Parse(s: ChannelId) : null,
			GuildId = GuildId != null ? ulong.Parse(s: GuildId) : null,
			FailIfNotExists = FailIfNotExists
		};
}