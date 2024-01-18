// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#message-object-message-structure
internal record DiscordMessageDto(
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
) : DiscordMessageBaseDto(Id: Id, ChannelId: ChannelId);