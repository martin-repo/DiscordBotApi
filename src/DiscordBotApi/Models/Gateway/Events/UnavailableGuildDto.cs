// -------------------------------------------------------------------------------------------------
// <copyright file="UnavailableGuildDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway.Events;

// https://discord.com/developers/docs/resources/guild#unavailable-guild-object
internal record UnavailableGuildDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "unavailable")]
	bool? Unavailable
);