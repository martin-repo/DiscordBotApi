// -------------------------------------------------------------------------------------------------
// <copyright file="BinaryWebSocket.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway
{
    using System.Net.WebSockets;

    using DiscordBotApi.Models.Gateway;

    internal class BinaryWebSocket : IBinaryWebSocket
    {
        private readonly SemaphoreSlim _sendAccess = new(1, 1);
        private readonly ClientWebSocket _webSocket;

        private bool _isDisconnecting;
        private bool _isDisposed;

        public BinaryWebSocket()
        {
            _webSocket = new ClientWebSocket();
        }

        public async Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(BinaryWebSocket));
            }

            if (_webSocket.State != WebSocketState.None)
            {
                throw new InvalidOperationException("Can only connect once per instance.");
            }

            await _webSocket.ConnectAsync(uri, cancellationToken).ConfigureAwait(false);
        }

        public async Task DisconnectAsync(DiscordGatewayCloseType closeType)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(BinaryWebSocket));
            }

            if (_webSocket.State != WebSocketState.Open)
            {
                return;
            }

            _isDisconnecting = true;
            await _webSocket.CloseAsync((WebSocketCloseStatus)closeType, null, CancellationToken.None).ConfigureAwait(false);
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _webSocket.Dispose();
            _isDisposed = true;

            GC.SuppressFinalize(this);
        }

        public async Task<byte[]> ReceiveAsync(CancellationToken cancellationToken)
        {
            await using var byteStream = new MemoryStream();

            var buffer = new byte[4096];
            WebSocketReceiveResult result;
            do
            {
                result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken).ConfigureAwait(false);
                if (result.MessageType == WebSocketMessageType.Binary)
                {
                    await byteStream.WriteAsync(buffer, 0, result.Count, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    break;
                }
            }
            while (!result.EndOfMessage);

            if (_isDisconnecting)
            {
                throw new OperationCanceledException("WebSocket is disconnected.");
            }

            switch (result.MessageType)
            {
                case WebSocketMessageType.Text:
                    throw new InvalidOperationException("WebSocket contained non-binary data.");
                case WebSocketMessageType.Binary:
                    return byteStream.ToArray();
                case WebSocketMessageType.Close:
                    throw new DiscordGatewayClosedException((DiscordGatewayCloseType?)(int?)result.CloseStatus, result.CloseStatusDescription);
                default:
                    throw new NotSupportedException($"{typeof(WebSocketMessageType)} {result.MessageType} is not supported");
            }
        }

        public async Task SendAsync(byte[] bytes, int chunkLength, CancellationToken cancellationToken)
        {
            await _sendAccess.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                var offset = 0;
                while (offset < bytes.Length)
                {
                    var count = bytes.Length - offset;
                    var segment = new ArraySegment<byte>(bytes, offset, count > chunkLength ? chunkLength : count);
                    offset += chunkLength;
                    var endOfMessage = offset >= bytes.Length;

                    await _webSocket.SendAsync(segment, WebSocketMessageType.Binary, endOfMessage, cancellationToken).ConfigureAwait(false);
                }
            }
            finally
            {
                _sendAccess.Release();
            }
        }
    }
}