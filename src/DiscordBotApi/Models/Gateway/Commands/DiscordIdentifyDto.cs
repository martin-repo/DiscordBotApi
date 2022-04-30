// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordIdentifyDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    using System.Text.Json.Serialization;

    internal record DiscordIdentifyDto(
        [property: JsonPropertyName("token")] string Token,
        [property: JsonPropertyName("properties")] DiscordGatewayConnectionPropertiesDto Properties,
        [property: JsonPropertyName("shard")] int[]? Shard,
        [property: JsonPropertyName("intents")] int Intents)
    {
        internal DiscordIdentifyDto(DiscordIdentify model)
            : this(
                model.Token,
                new DiscordGatewayConnectionPropertiesDto(model.Properties),
                model.Shard != null ? new[] { model.Shard.ShardId, model.Shard.NumShards } : null,
                model.Intents)
        {
        }
    }
}