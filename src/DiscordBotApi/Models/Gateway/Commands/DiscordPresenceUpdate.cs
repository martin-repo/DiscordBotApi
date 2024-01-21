// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPresenceUpdate.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands;

public record DiscordPresenceUpdate
{
	/// <summary>
	/// To set custom status for a bot; Type=Custom,Name="any",State="♥ Custom status message"
	/// </summary>
	public IReadOnlyCollection<DiscordActivityUpdate> Activities { get; init; } = Array.Empty<DiscordActivityUpdate>();

	public bool Afk { get; init; }

	public DateTime? Since { get; init; }

	public DiscordPresenceStatus Status { get; init; }
}