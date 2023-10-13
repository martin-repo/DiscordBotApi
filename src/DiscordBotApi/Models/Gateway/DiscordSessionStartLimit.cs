// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordSessionStartLimit.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway;

public record DiscordSessionStartLimit
{
	internal DiscordSessionStartLimit(DiscordSessionStartLimitDto dto)
	{
		Total = dto.Total;
		Remaining = dto.Remaining;
		ResetAfter = dto.ResetAfter;
		MaxConcurrency = dto.MaxConcurrency;
	}

	public int MaxConcurrency { get; init; }

	public int Remaining { get; init; }

	public int ResetAfter { get; init; }

	public int Total { get; init; }
}