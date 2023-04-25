// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildApplicationCommandPermissionsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway.Commands;

// https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-guild-application-command-permissions-structure
internal record DiscordGuildApplicationCommandPermissionsDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "application_id")]
	string ApplicationId,
	[property: JsonPropertyName(name: "guild_id")]
	string GuildId,
	[property: JsonPropertyName(name: "permissions")]
	DiscordApplicationCommandPermissionsDto[] Permissions
);