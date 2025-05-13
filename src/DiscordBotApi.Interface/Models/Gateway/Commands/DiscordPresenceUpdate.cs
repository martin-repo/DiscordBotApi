// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPresenceUpdate.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Gateway.Commands;

public sealed class DiscordPresenceUpdate
{
	/// <summary>
	/// To set custom status for a bot; Type=Custom,Name="any",State="♥ Custom status message"
	/// </summary>
	public required IReadOnlyCollection<DiscordActivityUpdate> Activities { get; init; } =
		Array.Empty<DiscordActivityUpdate>();

	public bool Afk { get; init; }

	public DateTime? Since { get; init; }

	public DiscordPresenceStatus Status { get; init; } = DiscordPresenceStatus.Online;
}