// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReactionRemove.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Gateway.Events;

public record DiscordMessageReactionRemove
{
	internal DiscordMessageReactionRemove(DiscordMessageReactionRemoveDto dto)
	{
		UserId = ulong.Parse(s: dto.UserId);
		ChannelId = ulong.Parse(s: dto.ChannelId);
		MessageId = ulong.Parse(s: dto.MessageId);
		GuildId = dto.GuildId != null
			? ulong.Parse(s: dto.GuildId)
			: null;
		Emoji = new DiscordEmoji(dto: dto.Emoji);
	}

	public ulong ChannelId { get; init; }

	public DiscordEmoji Emoji { get; init; }

	public ulong? GuildId { get; init; }

	public ulong MessageId { get; init; }

	public ulong UserId { get; init; }
}