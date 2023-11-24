// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Guilds;

internal record DiscordGuildMemberDto(
	[property: JsonPropertyName(name: "user")]
	DiscordUserDto? User,
	[property: JsonPropertyName(name: "nick")]
	string? Nick,
	[property: JsonPropertyName(name: "roles")]
	string[] Roles,
	[property: JsonPropertyName(name: "permissions")]
	string? Permissions
);