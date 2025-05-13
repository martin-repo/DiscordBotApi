// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordSessionStartLimit.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Gateway;

public sealed class DiscordSessionStartLimit
{
	public required int MaxConcurrency { get; init; }

	public required int Remaining { get; init; }

	public required int ResetAfter { get; init; }

	public required int Total { get; init; }
}