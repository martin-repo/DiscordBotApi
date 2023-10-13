// -------------------------------------------------------------------------------------------------
// <copyright file="BinaryWebSocket.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Net.WebSockets;

using DiscordBotApi.Models.Gateway;

namespace DiscordBotApi.Gateway;

internal class BinaryWebSocket : IBinaryWebSocket
{
	private readonly SemaphoreSlim _sendAccess = new(initialCount: 1, maxCount: 1);
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
			throw new ObjectDisposedException(objectName: nameof(BinaryWebSocket));
		}

		if (_webSocket.State != WebSocketState.None)
		{
			throw new InvalidOperationException(message: "Can only connect once per instance.");
		}

		await _webSocket.ConnectAsync(uri: uri, cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	public async Task DisconnectAsync(DiscordGatewayCloseType closeType)
	{
		if (_isDisposed)
		{
			throw new ObjectDisposedException(objectName: nameof(BinaryWebSocket));
		}

		if (_webSocket.State != WebSocketState.Open)
		{
			return;
		}

		_isDisconnecting = true;
		await _webSocket.CloseAsync(
				closeStatus: (WebSocketCloseStatus)closeType,
				statusDescription: null,
				cancellationToken: CancellationToken.None)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	public void Dispose()
	{
		if (_isDisposed)
		{
			return;
		}

		_webSocket.Dispose();
		_isDisposed = true;

		GC.SuppressFinalize(obj: this);
	}

	public async Task<byte[]> ReceiveAsync(CancellationToken cancellationToken)
	{
		await using var byteStream = new MemoryStream();

		var buffer = new byte[4096];
		WebSocketReceiveResult result;
		do
		{
			result = await _webSocket.ReceiveAsync(
					buffer: new ArraySegment<byte>(array: buffer),
					cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
			if (result.MessageType == WebSocketMessageType.Binary)
			{
				await byteStream.WriteAsync(
						buffer: buffer,
						offset: 0,
						count: result.Count,
						cancellationToken: cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
			}
			else
			{
				break;
			}
		} while (!result.EndOfMessage);

		if (_isDisconnecting)
		{
			throw new OperationCanceledException(message: "WebSocket is disconnected.");
		}

		switch (result.MessageType)
		{
			case WebSocketMessageType.Text:
				throw new InvalidOperationException(message: "WebSocket contained non-binary data.");
			case WebSocketMessageType.Binary:
				return byteStream.ToArray();
			case WebSocketMessageType.Close:
				throw new DiscordGatewayClosedException(
					closeType: (DiscordGatewayCloseType?)(int?)result.CloseStatus,
					closeStatusDescription: result.CloseStatusDescription);
			default:
				throw new NotSupportedException(message: $"{typeof(WebSocketMessageType)} {result.MessageType} is not supported");
		}
	}

	public async Task SendAsync(byte[] bytes, int chunkLength, CancellationToken cancellationToken)
	{
		await _sendAccess.WaitAsync(cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
		try
		{
			var offset = 0;
			while (offset < bytes.Length)
			{
				var count = bytes.Length - offset;
				var segment = new ArraySegment<byte>(
					array: bytes,
					offset: offset,
					count: count > chunkLength
						? chunkLength
						: count);
				offset += chunkLength;
				var endOfMessage = offset >= bytes.Length;

				await _webSocket.SendAsync(
						buffer: segment,
						messageType: WebSocketMessageType.Binary,
						endOfMessage: endOfMessage,
						cancellationToken: cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
			}
		}
		finally
		{
			_sendAccess.Release();
		}
	}
}