// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGateway.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway;

public record DiscordGateway
{
	internal DiscordGateway(DiscordGatewayDto dto)
	{
		Url = dto.Url;
	}

	public string Url { get; init; }
}