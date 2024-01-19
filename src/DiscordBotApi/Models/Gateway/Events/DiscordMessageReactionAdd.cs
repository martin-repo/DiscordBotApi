// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReactionAdd.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds;
using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Gateway.Events;

public record DiscordMessageReactionAdd
{
	internal DiscordMessageReactionAdd(DiscordMessageReactionAddDto dto)
	{
		UserId = ulong.Parse(s: dto.UserId);
		ChannelId = ulong.Parse(s: dto.ChannelId);
		MessageId = ulong.Parse(s: dto.MessageId);
		GuildId = dto.GuildId != null
			? ulong.Parse(s: dto.GuildId)
			: null;
		Member = dto.Member != null
			? new DiscordGuildMember(dto: dto.Member)
			: null;
		Emoji = new DiscordEmoji(dto: dto.Emoji);
		MessageAuthorId = dto.MessageAuthorId != null
			? ulong.Parse(s: dto.MessageAuthorId)
			: null;
	}

	public ulong ChannelId { get; init; }

	public DiscordEmoji Emoji { get; init; }

	public ulong? GuildId { get; init; }

	public DiscordGuildMember? Member { get; init; }

	public ulong? MessageAuthorId { get; init; }

	public ulong MessageId { get; init; }

	public ulong UserId { get; init; }
}