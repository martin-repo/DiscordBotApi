// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordIdentifyDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway.Commands;

internal record DiscordIdentifyDto(
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
	internal DiscordIdentifyDto(DiscordIdentify model) : this(
		Token: model.Token,
		Properties: new DiscordGatewayConnectionPropertiesDto(model: model.Properties),
		Shard: model.Shard != null
			? new[]
			{
				model.Shard.ShardId,
				model.Shard.NumShards
			}
			: null,
		Intents: model.Intents)
	{
	}
}