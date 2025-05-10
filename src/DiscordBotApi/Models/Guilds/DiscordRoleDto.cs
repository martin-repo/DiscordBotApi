// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRoleDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds;

namespace DiscordBotApi.Models.Guilds;

internal sealed record DiscordRoleDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "permissions")]
	string Permissions
)
{
	public DiscordRole ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			Name = Name,
			Permissions = (DiscordPermissions)ulong.Parse(s: Permissions)
		};
}