// -------------------------------------------------------------------------------------------------
// <copyright file="IBinaryWebSocket.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Gateway;

namespace DiscordBotApi.Gateway;

internal interface IBinaryWebSocket : IDisposable
{
	Task ConnectAsync(Uri uri, CancellationToken cancellationToken);

	Task DisconnectAsync(DiscordGatewayCloseType closeType);

	Task<byte[]> ReceiveAsync(CancellationToken cancellationToken);

	Task SendAsync(byte[] bytes, int chunkLength, CancellationToken cancellationToken);
}