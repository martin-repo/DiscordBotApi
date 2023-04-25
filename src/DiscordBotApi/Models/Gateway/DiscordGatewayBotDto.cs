// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayBotDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway;

// https://discord.com/developers/docs/topics/gateway#get-gateway-bot-json-response
internal record DiscordGatewayBotDto(
	[property: JsonPropertyName(name: "url")]
	string Url,
	[property: JsonPropertyName(name: "shards")]
	int Shards,
	[property: JsonPropertyName(name: "session_start_limit")]
	DiscordSessionStartLimitDto SessionStartLimit
);