// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReference.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

public record DiscordMessageReference
{
	public DiscordMessageReference()
	{
	}

	internal DiscordMessageReference(DiscordMessageReferenceDto dto)
	{
		MessageId = dto.MessageId != null
			? ulong.Parse(s: dto.MessageId)
			: null;
		ChannelId = dto.ChannelId != null
			? ulong.Parse(s: dto.ChannelId)
			: null;
		GuildId = dto.GuildId != null
			? ulong.Parse(s: dto.GuildId)
			: null;
		FailIfNotExists = dto.FailIfNotExists;
	}

	public ulong? ChannelId { get; init; }

	public bool? FailIfNotExists { get; init; }

	public ulong? GuildId { get; init; }

	public ulong? MessageId { get; init; }
}