﻿// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientGateway.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi
{
    using DiscordBotApi.Gateway;
    using DiscordBotApi.Models.Gateway;
    using DiscordBotApi.Models.Gateway.Commands;
    using DiscordBotApi.Models.Gateway.Events;

    public partial class DiscordBotClient
    {
        private readonly DiscordGatewayClient _gatewayClient;

        public event EventHandler<DiscordGatewayException>? GatewayException;

        public async Task<DiscordReady> ConnectToGatewayAsync(
            string gatewayUrl,
            DiscordGatewayIntent intents,
            DiscordShard? shard = null,
            CancellationToken cancellationToken = default)
        {
            var gatewayReady = new TaskCompletionSource<DiscordReady>(TaskCreationOptions.RunContinuationsAsynchronously);

            void OnGatewayReady(object? sender, DiscordReady ready)
            {
                gatewayReady.SetResult(ready);
            }

            _gatewayClient.GatewayReady += OnGatewayReady;
            try
            {
                await _gatewayClient.ConnectAsync(gatewayUrl, intents, shard, cancellationToken).ConfigureAwait(false);
                await gatewayReady.Task.ConfigureAwait(false);
            }
            finally
            {
                _gatewayClient.GatewayReady -= OnGatewayReady;
            }

            return gatewayReady.Task.Result;
        }

        public async Task DisconnectFromGatewayAsync(DiscordGatewayCloseType closeType = DiscordGatewayCloseType.NormalClosure)
        {
            await _gatewayClient.DisconnectAsync(closeType).ConfigureAwait(false);
        }

        // https://discord.com/developers/docs/topics/gateway#get-gateway
        public async Task<DiscordGateway> GetGatewayAsync(CancellationToken cancellationToken = default)
        {
            const string Url = "gateway";

            var gatewayDto = await _restClient
                                   .SendRequestAsync<DiscordGatewayDto>(() => new HttpRequestMessage(HttpMethod.Get, Url), cancellationToken)
                                   .ConfigureAwait(false);

            var gateway = new DiscordGateway(gatewayDto);

            return gateway;
        }

        // https://discord.com/developers/docs/topics/gateway#get-gateway-bot
        public async Task<DiscordGatewayBot> GetGatewayBotAsync(CancellationToken cancellationToken = default)
        {
            const string Url = "gateway/bot";

            var gatewayBotDto = await _restClient
                                      .SendRequestAsync<DiscordGatewayBotDto>(() => new HttpRequestMessage(HttpMethod.Get, Url), cancellationToken)
                                      .ConfigureAwait(false);

            var gatewayBot = new DiscordGatewayBot(gatewayBotDto);

            return gatewayBot;
        }

        public async Task UpdatePresenceAsync(DiscordPresenceUpdate presenceUpdate, CancellationToken cancellationToken = default)
        {
            await _gatewayClient.UpdatePresenceAsync(presenceUpdate, cancellationToken).ConfigureAwait(false);
        }
    }
}