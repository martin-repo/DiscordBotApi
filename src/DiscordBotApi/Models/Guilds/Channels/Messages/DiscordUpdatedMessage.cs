// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordUpdatedMessage.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

public record DiscordUpdatedMessage : DiscordMessageBase
{
	internal DiscordUpdatedMessage(DiscordBotClient botClient, DiscordUpdatedMessageDto dto) : base(
		botClient: botClient,
		id: dto.Id,
		channelId: dto.ChannelId)
	{
		GuildId = dto.GuildId != null
			? ulong.Parse(s: dto.GuildId)
			: null;
		Author = dto.Author != null
			? new DiscordUser(dto: dto.Author)
			: null;
		Content = dto.Content;
		Timestamp = dto.Timestamp != null
			? DateTime.Parse(s: dto.Timestamp)
			: null;
		EditedTimestamp = dto.EditedTimestamp != null
			? DateTime.Parse(s: dto.EditedTimestamp)
			: null;
		Attachments = dto.Attachments?.Select(selector: a => new DiscordMessageAttachment(dto: a))
			.ToArray();
		Embeds = dto.Embeds?.Select(selector: e => new DiscordEmbed(dto: e))
			.ToArray();
		Reactions = dto.Reactions?.Select(selector: r => new DiscordReaction(dto: r))
			.ToArray();
		Pinned = dto.Pinned;
		Thread = dto.Thread != null
			? new DiscordChannel(botClient: botClient, dto: dto.Thread)
			: null;
		Components = dto.Components?.Select(selector: c => new DiscordMessageActionRow(dto: c))
			.ToArray();
	}

	public IReadOnlyCollection<DiscordMessageAttachment>? Attachments { get; init; }

	public DiscordUser? Author { get; init; }

	public IReadOnlyCollection<DiscordMessageActionRow>? Components { get; init; }

	public string? Content { get; init; }

	public DateTime? EditedTimestamp { get; init; }

	public IReadOnlyCollection<DiscordEmbed>? Embeds { get; init; }

	public ulong? GuildId { get; init; }

	public bool? Pinned { get; init; }

	public IReadOnlyCollection<DiscordReaction>? Reactions { get; init; }

	public DiscordChannel? Thread { get; init; }

	public DateTime? Timestamp { get; init; }
}