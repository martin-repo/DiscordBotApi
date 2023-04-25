// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordThreadResponse.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#list-public-archived-threads-response-body
public record DiscordThreadResponse
{
	internal DiscordThreadResponse(DiscordBotClient botClient, DiscordThreadResponseDto dto)
	{
		Threads = dto.Threads.Select(selector: t => new DiscordChannel(botClient: botClient, dto: t))
			.ToArray();
		HasMore = dto.HasMore;
	}

	public bool HasMore { get; init; }

	public IReadOnlyCollection<DiscordChannel> Threads { get; init; }
}