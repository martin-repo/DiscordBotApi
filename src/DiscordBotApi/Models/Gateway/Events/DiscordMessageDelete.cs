// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageDelete.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events;

public record DiscordMessageDelete
{
	internal DiscordMessageDelete(DiscordMessageDeleteDto dto)
	{
		Id = ulong.Parse(s: dto.Id);
		ChannelId = ulong.Parse(s: dto.ChannelId);
		GuildId = dto.GuildId != null
			? ulong.Parse(s: dto.GuildId)
			: null;
	}

	public ulong ChannelId { get; init; }

	public ulong? GuildId { get; init; }

	public ulong Id { get; init; }
}