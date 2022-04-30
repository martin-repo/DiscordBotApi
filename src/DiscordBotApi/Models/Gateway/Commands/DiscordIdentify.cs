// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordIdentify.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    internal record DiscordIdentify(
        string Token,
        DiscordGatewayConnectionProperties Properties,
        DiscordShard? Shard,
        int Intents);
}