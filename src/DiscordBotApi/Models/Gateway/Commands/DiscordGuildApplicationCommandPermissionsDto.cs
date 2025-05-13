// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildApplicationCommandPermissionsDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Commands;

namespace DiscordBotApi.Models.Gateway.Commands;

// https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-guild-application-command-permissions-structure
internal sealed record DiscordGuildApplicationCommandPermissionsDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "application_id")]
	string ApplicationId,
	[property: JsonPropertyName(name: "guild_id")]
	string GuildId,
	[property: JsonPropertyName(name: "permissions")]
	DiscordApplicationCommandPermissionsDto[] Permissions
)
{
	public DiscordGuildApplicationCommandPermissions ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			ApplicationId = ulong.Parse(s: ApplicationId),
			GuildId = ulong.Parse(s: GuildId),
			Permissions = Permissions.Select(selector: p => p.ToModel()).ToArray()
		};
}