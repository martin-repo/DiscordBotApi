// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInstallParams.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds;

namespace DiscordBotApi.Models.Applications;

public record DiscordInstallParams
{
	internal DiscordInstallParams(DiscordInstallParamsDto dto)
	{
		Scopes = dto.Scopes;
		Permissions = (DiscordPermissions)ulong.Parse(s: dto.Permissions);
	}

	public DiscordPermissions Permissions { get; init; }

	public IReadOnlyCollection<string> Scopes { get; init; }
}