// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInstallParamsDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Applications;

// https://discord.com/developers/docs/resources/application#install-params-object-install-params-structure
internal record DiscordInstallParamsDto(
	[property: JsonPropertyName(name: "scopes")]
	string[] Scopes,
	[property: JsonPropertyName(name: "permissions")]
	string Permissions
);