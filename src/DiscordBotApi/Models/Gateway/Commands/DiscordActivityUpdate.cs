// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordActivityUpdate.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands;

public record DiscordActivityUpdate
{
	public string Name { get; init; } = "";

	public string? State { get; init; }

	public DiscordActivityType Type { get; init; }

	public string? Url { get; init; }
}