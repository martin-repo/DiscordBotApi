// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplication.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Users;

namespace DiscordBotApi.Interface.Models.Applications;

public sealed class DiscordApplication
{
	public required bool BotPublic { get; init; }

	public required bool BotRequireCodeGrant { get; init; }

	public string? CustomInstallUrl { get; init; }

	public DiscordApplicationFlags? Flags { get; init; }

	public required ulong Id { get; init; }

	public DiscordInstallParams? InstallParams { get; init; }

	public required string Name { get; init; }

	public DiscordUser? Owner { get; init; }

	public IReadOnlyCollection<string>? Tags { get; init; }
}