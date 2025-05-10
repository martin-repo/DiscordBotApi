// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberUpdate.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Users;

namespace DiscordBotApi.Interface.Models.Gateway.Events;

public sealed class DiscordGuildMemberUpdate
{
	public required ulong GuildId { get; init; }

	public string? Nick { get; init; }

	public required IReadOnlyCollection<ulong> Roles { get; init; }

	public required DiscordUser User { get; init; }
}