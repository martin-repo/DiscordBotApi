﻿// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessage.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;

using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

public record DiscordMessage : DiscordMessageBase
{
	internal DiscordMessage(DiscordBotClient botClient, DiscordMessageDto dto) : base(
		botClient: botClient,
		id: dto.Id,
		channelId: dto.ChannelId)
	{
		GuildId = dto.GuildId != null
			? ulong.Parse(s: dto.GuildId)
			: null;
		Author = new DiscordUser(dto: dto.Author);
		Content = dto.Content;
		Timestamp = DateTime.Parse(s: dto.Timestamp);
		EditedTimestamp = dto.EditedTimestamp != null
			? DateTime.Parse(s: dto.EditedTimestamp)
			: null;
		Mentions = dto.Mentions.Select(selector: m => new DiscordUser(dto: m))
			.ToImmutableArray();
		Attachments = dto.Attachments.Select(selector: a => new DiscordMessageAttachment(dto: a))
			.ToArray();
		Embeds = dto.Embeds.Select(selector: e => new DiscordEmbed(dto: e))
			.ToArray();
		Reactions = dto.Reactions?.Select(selector: r => new DiscordReaction(dto: r))
			.ToArray();
		Nonce = dto.Nonce;
		Pinned = dto.Pinned;
		Type = (DiscordMessageType)dto.Type;
		MessageReference = dto.MessageReference is not null
			? new DiscordMessageReference(dto: dto.MessageReference)
			: null;
		Flags = dto.Flags != null
			? (DiscordMessageFlags)dto.Flags
			: null;
		ReferencedMessage = dto.ReferencedMessage is not null
			? new DiscordMessage(botClient: botClient, dto: dto.ReferencedMessage)
			: null;
		Thread = dto.Thread != null
			? new DiscordChannel(botClient: botClient, dto: dto.Thread)
			: null;
		Components = dto.Components?.Select(selector: c => new DiscordMessageActionRow(dto: c))
			.ToArray();
		Member = dto.Member is not null
			? new DiscordGuildMember(dto: dto.Member)
			: null;
	}

	public IReadOnlyCollection<DiscordMessageAttachment> Attachments { get; init; }

	public DiscordUser Author { get; init; }

	public IReadOnlyCollection<DiscordMessageActionRow>? Components { get; init; }

	public string Content { get; init; }

	public DateTime? EditedTimestamp { get; init; }

	public IReadOnlyCollection<DiscordEmbed> Embeds { get; init; }

	public DiscordMessageFlags? Flags { get; init; }

	public ulong? GuildId { get; init; }

	public DiscordGuildMember? Member { get; init; }

	public ImmutableArray<DiscordUser> Mentions { get; init; }

	public DiscordMessageReference? MessageReference { get; init; }

	public string? Nonce { get; init; }

	public bool Pinned { get; init; }

	public IReadOnlyCollection<DiscordReaction>? Reactions { get; init; }

	public DiscordMessage? ReferencedMessage { get; init; }

	public DiscordChannel? Thread { get; init; }

	public DateTime Timestamp { get; init; }

	public DiscordMessageType Type { get; init; }
}