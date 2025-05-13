// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayBotDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Gateway;

namespace DiscordBotApi.Models.Gateway;

// https://discord.com/developers/docs/topics/gateway#get-gateway-bot-json-response
internal sealed record DiscordGatewayBotDto(
	[property: JsonPropertyName(name: "url")]
	string Url,
	[property: JsonPropertyName(name: "shards")]
	int Shards,
	[property: JsonPropertyName(name: "session_start_limit")]
	DiscordSessionStartLimitDto SessionStartLimit
)
{
	public DiscordGatewayBot ToModel() =>
		new()
		{
			Url = Url,
			Shards = Shards,
			SessionStartLimit = SessionStartLimit.ToModel()
		};
}