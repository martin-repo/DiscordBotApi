// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordActivityUpdate.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Gateway.Commands;

public sealed class DiscordActivityUpdate
{
	public required string Name { get; init; }

	public string? State { get; init; }

	public required DiscordActivityType Type { get; init; }

	public string? Url { get; init; }
}