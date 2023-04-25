// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Applications;

// https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-structure
internal record DiscordApplicationCommandDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "type")]
	int? Type,
	[property: JsonPropertyName(name: "application_id")]
	string ApplicationId,
	[property: JsonPropertyName(name: "guild_id")]
	string? GuildId,
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "description")]
	string Description,
	[property: JsonPropertyName(name: "options")]
	DiscordApplicationCommandOptionDto[]? Options,
	[property: JsonPropertyName(name: "default_member_permissions")]
	string? DefaultMemberPermissions,
	[property: JsonPropertyName(name: "dm_permission")]
	bool? DmPermission,
	[property: JsonPropertyName(name: "version")]
	string Version
);