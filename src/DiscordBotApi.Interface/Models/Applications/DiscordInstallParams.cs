// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInstallParams.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds;

namespace DiscordBotApi.Interface.Models.Applications;

public sealed class DiscordInstallParams
{
	public required DiscordPermissions Permissions { get; init; }

	public required IReadOnlyCollection<string> Scopes { get; init; }
}