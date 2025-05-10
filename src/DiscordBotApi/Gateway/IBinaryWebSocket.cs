// -------------------------------------------------------------------------------------------------
// <copyright file="IBinaryWebSocket.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Gateway;

namespace DiscordBotApi.Gateway;

internal interface IBinaryWebSocket : IDisposable
{
	Task ConnectAsync(Uri uri, CancellationToken cancellationToken);

	Task DisconnectAsync(DiscordGatewayCloseType closeType);

	Task<byte[]> ReceiveAsync(CancellationToken cancellationToken);

	Task SendAsync(byte[] bytes, int chunkLength, CancellationToken cancellationToken);
}