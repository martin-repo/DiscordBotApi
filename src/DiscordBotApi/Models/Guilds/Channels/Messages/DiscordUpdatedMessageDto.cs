﻿// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordUpdatedMessageDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

// https://discord.com/developers/docs/resources/channel#message-object-message-structure
// This class is a special-case for the "MESSAGE_UPDATE" gateway event where all fields can be null.
// https://discord.com/developers/docs/topics/gateway#message-update
internal record DiscordUpdatedMessageDto(
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
) : DiscordMessageBaseDto(Id: Id, ChannelId: ChannelId);