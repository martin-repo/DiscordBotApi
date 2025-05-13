// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageDto.cs" company="kpop.fan">
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
internal sealed record DiscordMessageDto(
	string Id,
	string ChannelId,
	[property: JsonPropertyName(name: "guild_id")]
	string? GuildId, // Exists in MESSAGE_CREATE and MESSAGE_UPDATE events
	[property: JsonPropertyName(name: "author")]
	DiscordUserDto Author,
	[property: JsonPropertyName(name: "content")]
	string Content,
	[property: JsonPropertyName(name: "timestamp")]
	string Timestamp,
	[property: JsonPropertyName(name: "edited_timestamp")]
	string? EditedTimestamp,
	[property: JsonPropertyName(name: "mentions")]
	DiscordUserDto[] Mentions,
	[property: JsonPropertyName(name: "attachments")]
	DiscordMessageAttachmentDto[] Attachments,
	[property: JsonPropertyName(name: "embeds")]
	DiscordEmbedDto[] Embeds,
	[property: JsonPropertyName(name: "reactions")]
	DiscordReactionDto[]? Reactions,
	[property: JsonPropertyName(name: "nonce")]
	string? Nonce,
	[property: JsonPropertyName(name: "pinned")]
	bool Pinned,
	[property: JsonPropertyName(name: "type")]
	int Type,
	[property: JsonPropertyName(name: "message_reference")]
	DiscordMessageReferenceDto? MessageReference,
	[property: JsonPropertyName(name: "flags")]
	int? Flags,
	[property: JsonPropertyName(name: "referenced_message")]
	DiscordMessageDto? ReferencedMessage,
	[property: JsonPropertyName(name: "thread")]
	DiscordChannelDto? Thread,
	[property: JsonPropertyName(name: "components")]
	DiscordMessageActionRowDto[]? Components,
	[property: JsonPropertyName(name: "member")]
	DiscordGuildMemberDto? Member // Exists in MESSAGE_CREATE and MESSAGE_UPDATE events
) : DiscordMessageBaseDto(Id: Id, ChannelId: ChannelId)
{
	public DiscordMessage ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			ChannelId = ulong.Parse(s: ChannelId),
			GuildId = GuildId != null ? ulong.Parse(s: GuildId) : null,
			Author = Author.ToModel(),
			Content = Content,
			Timestamp = DateTime.Parse(s: Timestamp),
			EditedTimestamp = EditedTimestamp != null ? DateTime.Parse(s: EditedTimestamp) : null,
			Mentions = Mentions.Select(selector: m => m.ToModel()).ToImmutableArray(),
			Attachments = Attachments.Select(selector: a => a.ToModel()).ToArray(),
			Embeds = Embeds.Select(selector: e => e.ToModel()).ToArray(),
			Reactions = Reactions?.Select(selector: r => r.ToModel()).ToArray(),
			Nonce = Nonce,
			Pinned = Pinned,
			Type = (DiscordMessageType)Type,
			MessageReference = MessageReference?.ToModel(),
			Flags = Flags != null ? (DiscordMessageFlags)Flags : null,
			ReferencedMessage = ReferencedMessage?.ToModel(),
			Thread = Thread?.ToModel(),
			Components = Components?.Select(selector: c => c.ToModel()).ToArray(),
			Member = Member?.ToModel()
		};
}