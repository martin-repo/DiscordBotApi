// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReferenceDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#message-reference-object
internal record DiscordMessageReferenceDto(
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
	internal DiscordMessageReferenceDto(DiscordMessageReference model) : this(
		MessageId: model.MessageId?.ToString(),
		ChannelId: model.ChannelId?.ToString(),
		GuildId: model.GuildId?.ToString(),
		FailIfNotExists: model.FailIfNotExists)
	{
	}
}