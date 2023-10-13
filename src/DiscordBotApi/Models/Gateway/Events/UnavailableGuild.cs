// -------------------------------------------------------------------------------------------------
// <copyright file="UnavailableGuild.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events;

public record UnavailableGuild
{
	internal UnavailableGuild(UnavailableGuildDto dto)
	{
		Id = ulong.Parse(s: dto.Id);
		Unavailable = dto.Unavailable;
	}

	public ulong Id { get; init; }

	public bool? Unavailable { get; init; }
}