// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordUserDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Users;

internal record DiscordUserDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "username")]
	string Username,
	[property: JsonPropertyName(name: "discriminator")]
	string Discriminator,
	[property: JsonPropertyName(name: "global_name")]
	string? GlobalName,
	[property: JsonPropertyName(name: "avatar")]
	string? Avatar,
	[property: JsonPropertyName(name: "bot")]
	bool? Bot
);