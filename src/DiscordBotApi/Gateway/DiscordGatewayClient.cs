// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayClient.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

using DiscordBotApi.Interface.Gateway;
using DiscordBotApi.Interface.Models.Gateway;
using DiscordBotApi.Interface.Models.Gateway.Commands;
using DiscordBotApi.Interface.Models.Gateway.Events;
using DiscordBotApi.Models.Gateway;

using Serilog;

namespace DiscordBotApi.Gateway;

internal partial class DiscordGatewayClient(
	ILogger? logger,
	Func<IBinaryWebSocket> webSocketActivator,
	Func<IZlibContext> zlibContextActivator,
	string botToken
) : IAsyncDisposable
{
	// https://discord.com/developers/docs/topics/gateway#sending-payloads
	private const int OutgoingWebSocketChunkLength = 4096;

	private static readonly TextInfo EnglishTextInfo = new CultureInfo(name: "en-US", useUserOverride: false).TextInfo;

	private readonly ILogger? _logger = logger?.ForContext<DiscordGatewayClient>();

	private bool _isDisposed;

	private DiscordGatewaySession _session = new(
		webSocket: default!,
		zlibContext: default!,
		gatewayUrl: "",
		intents: DiscordGatewayIntent.None,
		shard: null
	) { Status = DiscordGatewaySessionStatus.Disconnected };

	public event EventHandler<DiscordGatewayDispatch>? GatewayDispatchReceived;

	public event EventHandler<DiscordGatewayException>? GatewayException;

	public event EventHandler<DiscordReady>? GatewayReady;

	public async Task ConnectAsync(
		string gatewayUrl,
		DiscordGatewayIntent intents,
		DiscordShard? shard = null,
		CancellationToken cancellationToken = default
	)
	{
		if (_isDisposed)
		{
			throw new ObjectDisposedException(objectName: $"{nameof(DiscordGatewayClient)} has been disposed.");
		}

		await ConnectAsync(
				gatewayUrl: gatewayUrl,
				intents: intents,
				sessionId: null,
				sessionSequenceNumber: 0,
				shard: shard,
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	public async Task DisconnectAsync(DiscordGatewayCloseType closeType)
	{
		if (_isDisposed)
		{
			throw new ObjectDisposedException(objectName: $"{nameof(DiscordGatewayClient)} has been disposed.");
		}

		await DisconnectInternalAsync(closeType: closeType).ConfigureAwait(continueOnCapturedContext: false);
	}

	public async ValueTask DisposeAsync()
	{
		await DisconnectInternalAsync(closeType: DiscordGatewayCloseType.NormalClosure)
			.ConfigureAwait(continueOnCapturedContext: false);

		_isDisposed = true;

		GC.SuppressFinalize(obj: this);
	}

	public async Task UpdatePresenceAsync(
		DiscordPresenceUpdate presenceUpdate,
		CancellationToken cancellationToken = default
	)
	{
		if (_session.Status != DiscordGatewaySessionStatus.Connected)
		{
			// Discord will regularly disconnect clients in order to move them to a different server.
			// The reconnect logic will handle this, but it might take a second.
			// If this method happens to be called during the reconnect, it will throw an InvalidOperationException.
			// A possible improvement might be to detect this particular situation, and wait here with a timeout.
			// But I think it's better to let the caller handle this case.
			throw new InvalidOperationException(message: "Not connected");
		}

		await SendPresenceUpdateAsync(presenceUpdate: presenceUpdate, cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	private static float GetJitter()
	{
		var random = new Random();
		var jitter = random.NextSingle();
		while (jitter == 0)
		{
			jitter = random.NextSingle();
		}

		return jitter;
	}

	private static async Task JoinWithTaskAsync(Task? task)
	{
		if (task == null)
		{
			return;
		}

		try
		{
			await task
				.WaitAsync(cancellationToken: CancellationToken.None)
				.ConfigureAwait(continueOnCapturedContext: false);
		}
		catch (OperationCanceledException)
		{
		}
	}

	private async Task ConnectAsync(
		string gatewayUrl,
		DiscordGatewayIntent intents,
		string? sessionId = null,
		int? sessionSequenceNumber = null,
		DiscordShard? shard = null,
		CancellationToken cancellationToken = default
	)
	{
		if (_session.Status != DiscordGatewaySessionStatus.Disconnected)
		{
			throw new InvalidOperationException(message: "Already connected");
		}

		_session = new DiscordGatewaySession(
			webSocket: webSocketActivator(),
			zlibContext: zlibContextActivator(),
			gatewayUrl: gatewayUrl,
			intents: intents,
			shard: shard
		)
		{
			Id = sessionId ?? DiscordGatewaySession.EmptySessionId,
			SequenceNumber = sessionSequenceNumber ?? 0
		};

		_logger?.Debug(messageTemplate: "Gateway -- Connecting");

		var uri = new Uri(
			uriString: $"{gatewayUrl}?v={DiscordBotClient.DiscordApiVersion}&encoding=json&compress=zlib-stream"
		);

		try
		{
			await _session
				.WebSocket.ConnectAsync(uri: uri, cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
			_session.PayloadLoopTask = Task
				.Run(
					function: async () => await PayloadLoopAsync().ConfigureAwait(continueOnCapturedContext: false),
					cancellationToken: cancellationToken
				)
				.ContinueWith(
					continuationFunction: async task =>
						await HandlePayloadLoopException(loopException: task.Exception!.InnerExceptions.First())
							.ConfigureAwait(continueOnCapturedContext: false),
					continuationOptions: TaskContinuationOptions.OnlyOnFaulted
				);
		}
		catch
		{
			await DisconnectInternalAsync(closeType: DiscordGatewayCloseType.UnknownError)
				.ConfigureAwait(continueOnCapturedContext: false);
			throw;
		}
	}

	private async Task DisconnectInternalAsync(DiscordGatewayCloseType closeType)
	{
		if (_session.Status != DiscordGatewaySessionStatus.Connected)
		{
			return;
		}

		_logger?.Debug(messageTemplate: "Gateway -- Disconnecting");

		_session.Status = DiscordGatewaySessionStatus.Disconnecting;

		// Cancel heartbeat loop to ensure nothing is written to the socket while
		// disconnect is in progress.
		await _session.HeartbeatCancellationSource.CancelAsync().ConfigureAwait(continueOnCapturedContext: false);

		// Close socket before cancelling payload loop to ensure that closeType
		// is properly transmitted to Discord.
		await _session.WebSocket.DisconnectAsync(closeType: closeType).ConfigureAwait(continueOnCapturedContext: false);

		await _session.PayloadCancellationSource.CancelAsync().ConfigureAwait(continueOnCapturedContext: false);

		await JoinWithTaskAsync(task: _session.HeartbeatLoopTask).ConfigureAwait(continueOnCapturedContext: false);
		await JoinWithTaskAsync(task: _session.PayloadLoopTask).ConfigureAwait(continueOnCapturedContext: false);

		_session.WebSocket.Dispose();

		_session.Status = DiscordGatewaySessionStatus.Disconnected;
	}

	private async Task HeartbeatLoopAsync(TimeSpan interval)
	{
		var cancellationToken = _session.HeartbeatCancellationSource.Token;

		var jitter = GetJitter();
		var initialInterval = TimeSpan.FromMilliseconds(value: interval.TotalMilliseconds * jitter);

		var nextHeartbeat = DateTime.UtcNow.Add(value: initialInterval);
		while (!cancellationToken.IsCancellationRequested)
		{
			var delay = nextHeartbeat - DateTime.UtcNow;
			if (delay > TimeSpan.Zero)
			{
				await Task
					.Delay(delay: delay, cancellationToken: cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
			}

			if (!_session.HeartbeatAckReceived)
			{
				_logger?.Warning(messageTemplate: "No heartbeat ack was received");
				Reconnect(closeType: DiscordGatewayCloseType.UnknownError);
				return;
			}

			await SendHeartbeatAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);

			do
			{
				nextHeartbeat = nextHeartbeat.Add(value: interval);
			}
			while (nextHeartbeat < DateTime.UtcNow);
		}
	}

	private async Task PayloadLoopAsync()
	{
		var cancellationToken = _session.PayloadCancellationSource.Token;

		var socket = _session.WebSocket;
		var zlib = _session.ZlibContext;

		while (!cancellationToken.IsCancellationRequested)
		{
			var socketBytes = await socket
				.ReceiveAsync(cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
			var payloadBytes = await zlib
				.DecompressAsync(compressedBytes: socketBytes, cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
			var payloadJson = Encoding.UTF8.GetString(bytes: payloadBytes);

			var payloadDto = JsonSerializer.Deserialize<DiscordGatewayPayloadDto>(json: payloadJson);
			if (payloadDto == null)
			{
				throw new SerializationException(message: $"Failed to deserialize {nameof(DiscordGatewayPayload)}.");
			}

			var payload = payloadDto.ToModel();
			if (payload.SequenceNumber != null)
			{
				_session.SequenceNumber = payload.SequenceNumber.Value;
			}

			await HandlePayloadAsync(payload: payload, cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);
		}
	}

	private void Reconnect(DiscordGatewayCloseType closeType) =>
		_ = Task
			.Run(
				function: async () =>
				{
					await DisconnectInternalAsync(closeType: closeType).ConfigureAwait(continueOnCapturedContext: false);
					await ConnectAsync(
							gatewayUrl: _session.GatewayUrl,
							intents: _session.Intents,
							sessionId: _session.Id,
							sessionSequenceNumber: _session.SequenceNumber,
							shard: _session.Shard
						)
						.ConfigureAwait(continueOnCapturedContext: false);
				}
			)
			.ContinueWith(
				continuationAction: task =>
				{
					var gatewayException = new DiscordGatewayException(
						message: "Reconnect failed",
						isDisconnected: true,
						innerException: task.Exception!.InnerExceptions.First()
					);
					GatewayException?.Invoke(sender: this, e: gatewayException);
				},
				continuationOptions: TaskContinuationOptions.OnlyOnFaulted
			);
}