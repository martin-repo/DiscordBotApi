﻿// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordActivityUpdateDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway.Commands;

// https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-structure
internal record DiscordActivityUpdateDto(
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "type")]
	int Type,
	[property: JsonPropertyName(name: "url")]
	string? Url,
	[property: JsonPropertyName(name: "state")]
	string? State
)
{
	internal DiscordActivityUpdateDto(DiscordActivityUpdate model) : this(
		Name: model.Name,
		Type: (int)model.Type,
		Url: model.Url,
		State: model.State)
	{
	}
}