// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordIdentifyDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway.Commands;

internal sealed record DiscordIdentifyDto(
	[property: JsonPropertyName(name: "token")]
	string Token,
	[property: JsonPropertyName(name: "properties")]
	DiscordGatewayConnectionPropertiesDto Properties,
	[property: JsonPropertyName(name: "shard")]
	int[]? Shard,
	[property: JsonPropertyName(name: "intents")]
	int Intents
)
{
	public static DiscordIdentifyDto FromModel(DiscordIdentify model) =>
		new(
			Token: model.Token,
			Properties: DiscordGatewayConnectionPropertiesDto.FromModel(model: model.Properties),
			Shard: model.Shard != null ? [model.Shard.ShardId, model.Shard.NumShards] : null,
			Intents: model.Intents
		);
}