// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Applications;

// https://discord.com/developers/docs/resources/application#application-object-application-structure
internal record DiscordApplicationDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "bot_public")]
	bool BotPublic,
	[property: JsonPropertyName(name: "bot_require_code_grant")]
	bool BotRequireCodeGrant,
	[property: JsonPropertyName(name: "owner")]
	DiscordUserDto? Owner,
	[property: JsonPropertyName(name: "flags")]
	int? Flags,
	[property: JsonPropertyName(name: "tags")]
	string[]? Tags,
	[property: JsonPropertyName(name: "install_params")]
	DiscordInstallParamsDto? InstallParams,
	[property: JsonPropertyName(name: "custom_install_url")]
	string[]? CustomInstallUrl
);