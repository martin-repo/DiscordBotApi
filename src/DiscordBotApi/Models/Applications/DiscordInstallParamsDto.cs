// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInstallParamsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Applications;
using DiscordBotApi.Interface.Models.Guilds;

namespace DiscordBotApi.Models.Applications;

// https://discord.com/developers/docs/resources/application#install-params-object-install-params-structure
internal sealed record DiscordInstallParamsDto(
	[property: JsonPropertyName(name: "scopes")]
	string[] Scopes,
	[property: JsonPropertyName(name: "permissions")]
	string Permissions
)
{
	public DiscordInstallParams ToModel() =>
		new()
		{
			Scopes = Scopes.ToImmutableArray(),
			Permissions = (DiscordPermissions)ulong.Parse(s: Permissions)
		};
}