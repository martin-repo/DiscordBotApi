// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayBot.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway;

public record DiscordGatewayBot
{
	internal DiscordGatewayBot(DiscordGatewayBotDto dto)
	{
		Url = dto.Url;
		Shards = dto.Shards;
		SessionStartLimit = new DiscordSessionStartLimit(dto: dto.SessionStartLimit);
	}

	public DiscordSessionStartLimit SessionStartLimit { get; init; }

	public int Shards { get; init; }

	public string Url { get; init; }
}