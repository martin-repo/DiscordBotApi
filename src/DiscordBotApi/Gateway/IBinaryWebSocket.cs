// -------------------------------------------------------------------------------------------------
// <copyright file="IBinaryWebSocket.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway
{
    using DiscordBotApi.Models.Gateway;

    internal interface IBinaryWebSocket : IDisposable
    {
        Task ConnectAsync(Uri uri, CancellationToken cancellationToken);

        Task DisconnectAsync(DiscordGatewayCloseType closeType);

        Task<byte[]> ReceiveAsync(CancellationToken cancellationToken);

        Task SendAsync(byte[] bytes, int chunkLength, CancellationToken cancellationToken);
    }
}