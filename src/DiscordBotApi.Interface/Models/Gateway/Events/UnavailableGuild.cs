// -------------------------------------------------------------------------------------------------
// <copyright file="UnavailableGuild.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Gateway.Events;

public sealed class UnavailableGuild
{
	public required ulong Id { get; init; }

	public bool? Unavailable { get; init; }
}