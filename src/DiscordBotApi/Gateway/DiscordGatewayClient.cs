// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayClient.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway
{
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Text.Json;

    using DiscordBotApi.Models.Gateway;
    using DiscordBotApi.Models.Gateway.Commands;
    using DiscordBotApi.Models.Gateway.Events;

    using Serilog;

    internal partial class DiscordGatewayClient : IAsyncDisposable
    {
        // https://discord.com/developers/docs/topics/gateway#sending-payloads
        private const int OutgoingWebSocketChunkLength = 4096;

        private static readonly TextInfo EnglishTextInfo = new CultureInfo("en-US", false).TextInfo;

        private readonly string _botToken;
        private readonly ILogger? _logger;
        private readonly Func<IBinaryWebSocket> _webSocketActivator;
        private readonly Func<IZlibContext> _zlibContextActivator;

        private bool _isDisposed;
        private DiscordGatewaySession _session;

        public DiscordGatewayClient(
            ILogger? logger,
            Func<IBinaryWebSocket> webSocketActivator,
            Func<IZlibContext> zlibContextActivator,
            string botToken)
        {
            _logger = logger?.ForContext<DiscordGatewayClient>();
            _webSocketActivator = webSocketActivator;
            _zlibContextActivator = zlibContextActivator;
            _botToken = botToken;
            _session = new DiscordGatewaySession(default!, default!, "", DiscordGatewayIntent.None, null)
                       {
                           Status = DiscordGatewaySessionStatus.Disconnected
                       };
        }

        public event EventHandler<DiscordGatewayDisconnect>? GatewayDisconnected;

        public event EventHandler<DiscordGatewayDispatch>? GatewayDispatchReceived;

        public event EventHandler<DiscordGatewayException>? GatewayException;

        public event EventHandler<DiscordReady>? GatewayReady;

        public async Task ConnectAsync(string gatewayUrl, DiscordGatewayIntent intents, DiscordShard? shard = null)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException($"{nameof(DiscordGatewayClient)} has been disposed.");
            }

            await ConnectAsync(gatewayUrl, intents, null, 0, shard).ConfigureAwait(false);
        }

        public async Task DisconnectAsync(DiscordGatewayCloseType closeType)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException($"{nameof(DiscordGatewayClient)} has been disposed.");
            }

            await DisconnectInternalAsync(closeType).ConfigureAwait(false);
        }

        public async ValueTask DisposeAsync()
        {
            await DisconnectInternalAsync(DiscordGatewayCloseType.NormalClosure).ConfigureAwait(false);

            _isDisposed = true;

            GC.SuppressFinalize(this);
        }

        public async Task UpdatePresenceAsync(DiscordPresenceUpdate presenceUpdate)
        {
            if (_session.Status != DiscordGatewaySessionStatus.Connected)
            {
                throw new InvalidOperationException("Not connected");
            }

            await SendPresenceUpdateAsync(presenceUpdate).ConfigureAwait(false);
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
                await task.WaitAsync(CancellationToken.None).ConfigureAwait(false);
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
            DiscordShard? shard = null)
        {
            if (_session.Status != DiscordGatewaySessionStatus.Disconnected)
            {
                throw new InvalidOperationException("Already connected");
            }

            _session = new DiscordGatewaySession(_webSocketActivator(), _zlibContextActivator(), gatewayUrl, intents, shard)
                       {
                           Id = sessionId ?? DiscordGatewaySession.EmptySessionId, SequenceNumber = sessionSequenceNumber ?? 0
                       };

            _logger?.Information("Gateway -- Connecting");

            var uri = new Uri($"{gatewayUrl}?v={DiscordBotClient.DiscordApiVersion}&encoding=json&compress=zlib-stream");
            await _session.WebSocket.ConnectAsync(uri, CancellationToken.None).ConfigureAwait(false);

            _session.PayloadLoopTask = Task.Run(async () => await PayloadLoopAsync().ConfigureAwait(false))
                                           .ContinueWith(
                                               async task => await HandlePayloadLoopException(task.Exception!.InnerExceptions.First())
                                                                 .ConfigureAwait(false),
                                               TaskContinuationOptions.OnlyOnFaulted);
        }

        private async Task DisconnectInternalAsync(DiscordGatewayCloseType closeType)
        {
            if (_session.Status != DiscordGatewaySessionStatus.Connected)
            {
                return;
            }

            _logger?.Debug("Gateway -- Disconnecting");

            _session.Status = DiscordGatewaySessionStatus.Disconnecting;

            // Cancel heartbeat loop to ensure nothing is written to the socket while
            // disconnect is in progress.
            _session.HeartbeatCancellationSource.Cancel();

            // Close socket before cancelling payload loop to ensure that closeType
            // is properly transmitted to Discord.
            await _session.WebSocket.DisconnectAsync(closeType).ConfigureAwait(false);

            _session.PayloadCancellationSource.Cancel();

            await JoinWithTaskAsync(_session.HeartbeatLoopTask).ConfigureAwait(false);
            await JoinWithTaskAsync(_session.PayloadLoopTask).ConfigureAwait(false);

            _session.WebSocket.Dispose();

            _session.Status = DiscordGatewaySessionStatus.Disconnected;
        }

        private async Task HeartbeatLoopAsync(TimeSpan interval)
        {
            var cancellationToken = _session.HeartbeatCancellationSource.Token;

            var jitter = GetJitter();
            var initialInterval = TimeSpan.FromMilliseconds(interval.TotalMilliseconds * jitter);

            var nextHeartbeat = DateTime.UtcNow.Add(initialInterval);
            while (!cancellationToken.IsCancellationRequested)
            {
                var delay = nextHeartbeat - DateTime.UtcNow;
                if (delay > TimeSpan.Zero)
                {
                    await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                }

                if (!_session.HeartbeatAckReceived)
                {
                    _logger?.Warning("No heartbeat ack was received");
                    Reconnect(DiscordGatewayCloseType.UnknownError);
                    return;
                }

                await SendHeartbeatAsync(cancellationToken).ConfigureAwait(false);

                do
                {
                    nextHeartbeat = nextHeartbeat.Add(interval);
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
                var socketBytes = await socket.ReceiveAsync(cancellationToken).ConfigureAwait(false);
                var payloadBytes = await zlib.DecompressAsync(socketBytes, cancellationToken).ConfigureAwait(false);
                var payloadJson = Encoding.UTF8.GetString(payloadBytes);

                var payloadDto = JsonSerializer.Deserialize<DiscordGatewayPayloadDto>(payloadJson);
                if (payloadDto == null)
                {
                    throw new SerializationException($"Failed to deserialize {nameof(DiscordGatewayPayload)}.");
                }

                var payload = new DiscordGatewayPayload(payloadDto);
                if (payload.SequenceNumber != null)
                {
                    _session.SequenceNumber = payload.SequenceNumber.Value;
                }

                await HandlePayloadAsync(payload, cancellationToken).ConfigureAwait(false);
            }
        }

        private void Reconnect(DiscordGatewayCloseType closeType)
        {
            _ = Task.Run(
                        async () =>
                        {
                            await DisconnectInternalAsync(closeType).ConfigureAwait(false);
                            await ConnectAsync(_session.GatewayUrl, _session.Intents, _session.Id, _session.SequenceNumber, _session.Shard)
                                .ConfigureAwait(false);
                        })
                    .ContinueWith(
                        task =>
                        {
                            var gatewayException = new DiscordGatewayException("Reconnect failed", task.Exception!.InnerExceptions.First());
                            GatewayException?.Invoke(this, gatewayException);
                        },
                        TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}