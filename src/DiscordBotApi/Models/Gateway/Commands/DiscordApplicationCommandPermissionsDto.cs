// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandPermissionsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Commands;

namespace DiscordBotApi.Models.Gateway.Commands;

// https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-application-command-permissions-structure
internal sealed record DiscordApplicationCommandPermissionsDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "type")]
	int Type,
	[property: JsonPropertyName(name: "permission")]
	bool Permission
)
{
	public DiscordApplicationCommandPermissions ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			Type = (DiscordApplicationCommandPermissionType)Type,
			Permission = Permission
		};
}