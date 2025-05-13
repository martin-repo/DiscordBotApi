// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordActivityUpdateDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway.Commands;

namespace DiscordBotApi.Models.Gateway.Commands;

// https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-structure
internal sealed record DiscordActivityUpdateDto(
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
	public static DiscordActivityUpdateDto FromModel(DiscordActivityUpdate model) =>
		new(
			Name: model.Name,
			Type: (int)model.Type,
			Url: model.Url,
			State: model.State
		);
}