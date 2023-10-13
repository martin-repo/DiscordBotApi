// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordActiveThreadResponse.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#list-public-archived-threads-response-body
public record DiscordActiveThreadResponse
{
	internal DiscordActiveThreadResponse(DiscordBotClient botClient, DiscordActiveThreadResponseDto dto)
	{
		Threads = dto.Threads.Select(selector: t => new DiscordChannel(botClient: botClient, dto: t))
			.ToArray();
	}

	public IReadOnlyCollection<DiscordChannel> Threads { get; init; }
}