// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReady.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Applications;
using DiscordBotApi.Interface.Models.Gateway.Commands;
using DiscordBotApi.Interface.Models.Users;

namespace DiscordBotApi.Interface.Models.Gateway.Events;

public sealed class DiscordReady
{
	public required DiscordApplication Application { get; init; }

	public required IReadOnlyCollection<UnavailableGuild> Guilds { get; init; }

	public required string ResumeGatewayUrl { get; init; }

	public required string SessionId { get; init; }

	public DiscordShard? Shard { get; init; }

	public required DiscordUser User { get; init; }

	public required int V { get; init; }
}