// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordUpdatedMessageDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#message-object-message-structure
// This class is a special-case for the "MESSAGE_UPDATE" gateway event where all fields can be null.
// https://discord.com/developers/docs/topics/gateway#message-update
internal sealed record DiscordUpdatedMessageDto(
	string Id,
	string ChannelId,
	[property: JsonPropertyName(name: "guild_id")]
	string? GuildId,
	[property: JsonPropertyName(name: "author")]
	DiscordUserDto? Author,
	[property: JsonPropertyName(name: "content")]
	string? Content,
	[property: JsonPropertyName(name: "timestamp")]
	string? Timestamp,
	[property: JsonPropertyName(name: "edited_timestamp")]
	string? EditedTimestamp,
	[property: JsonPropertyName(name: "attachments")]
	DiscordMessageAttachmentDto[]? Attachments,
	[property: JsonPropertyName(name: "embeds")]
	DiscordEmbedDto[]? Embeds,
	[property: JsonPropertyName(name: "reactions")]
	DiscordReactionDto[]? Reactions,
	[property: JsonPropertyName(name: "pinned")]
	bool? Pinned,
	[property: JsonPropertyName(name: "thread")]
	DiscordChannelDto? Thread,
	[property: JsonPropertyName(name: "components")]
	DiscordMessageActionRowDto[]? Components
) : DiscordMessageBaseDto(Id: Id, ChannelId: ChannelId)
{
	public DiscordUpdatedMessage ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			ChannelId = ulong.Parse(s: ChannelId),
			GuildId = GuildId != null ? ulong.Parse(s: GuildId) : null,
			Author = Author?.ToModel(),
			Content = Content,
			Timestamp = Timestamp != null ? DateTime.Parse(s: Timestamp) : null,
			EditedTimestamp = EditedTimestamp != null ? DateTime.Parse(s: EditedTimestamp) : null,
			Attachments = Attachments?.Select(selector: a => a.ToModel()).ToImmutableArray(),
			Embeds = Embeds?.Select(selector: e => e.ToModel()).ToImmutableArray(),
			Reactions = Reactions?.Select(selector: r => r.ToModel()).ToImmutableArray(),
			Pinned = Pinned,
			Thread = Thread?.ToModel(),
			Components = Components?.Select(selector: c => c.ToModel()).ToImmutableArray()
		};
}