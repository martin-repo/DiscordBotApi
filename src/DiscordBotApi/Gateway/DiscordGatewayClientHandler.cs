// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayClientHandler.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway
{
    using System.Net.Sockets;
    using System.Net.WebSockets;
    using System.Text.Json;

    using DiscordBotApi.Models.Gateway;
    using DiscordBotApi.Models.Gateway.Commands;
    using DiscordBotApi.Models.Gateway.Events;

    internal partial class DiscordGatewayClient
    {
        private const int ConnectRetryIntervalSeconds = 2;
        private const int MaxConnectAttempts = 60;

        private async Task ConnectUntilSuccessfulAsync(
            string gatewayUrl,
            DiscordGatewayIntent intents,
            string? sessionId,
            int? sessionSequenceNumber,
            DiscordShard? shard)
        {
            var connectAttempts = 0;
            while (!_isDisposed)
            {
                try
                {
                    _logger?.Debug("Attempt {Index} to connect", connectAttempts + 1);
                    await ConnectAsync(gatewayUrl, intents, sessionId, sessionSequenceNumber, shard).ConfigureAwait(false);
                    return;
                }
                catch (Exception exception) when (ShouldConnectAgain(exception))
                {
                    if (++connectAttempts >= MaxConnectAttempts)
                    {
                        GatewayException?.Invoke(this, new DiscordGatewayException($"{nameof(ConnectAsync)} failed", exception));
                        return;
                    }

                    await Task.Delay(TimeSpan.FromSeconds(ConnectRetryIntervalSeconds)).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    GatewayException?.Invoke(this, new DiscordGatewayException($"{nameof(ConnectAsync)} failed", exception));
                    return;
                }
            }
        }

        private async Task HandleDiscordGatewayClosedExceptionAsync(DiscordGatewaySession session, DiscordGatewayClosedException closedException)
        {
            _logger?.Debug("Gateway -- Disconnected ({CloseType})", closedException.CloseType);
            if (ShouldReconnect(closedException.CloseType))
            {
                _logger?.Debug("Attempt to reconnect (reason: {Reason})", closedException.CloseType);

                await ConnectUntilSuccessfulAsync(session.GatewayUrl, session.Intents, session.Id, session.SequenceNumber, session.Shard)
                    .ConfigureAwait(false);
            }
            else
            {
                _logger?.Debug("Will not reconnect (reason: {Reason})", closedException.CloseType);

                GatewayDisconnected?.Invoke(this, new DiscordGatewayDisconnect(closedException.CloseType, closedException.CloseStatusDescription));
            }
        }

        private void HandleDispatch(DiscordGatewayPayload payload)
        {
            var eventType = ParseEventType(payload.EventName);
            if (eventType == null)
            {
                return;
            }

            switch (eventType)
            {
                case DiscordEventType.ApplicationCommandPermissionsUpdate:
                case DiscordEventType.ChannelCreate:
                case DiscordEventType.ChannelUpdate:
                case DiscordEventType.ChannelDelete:
                case DiscordEventType.ChannelPinsUpdate:
                case DiscordEventType.GuildCreate:
                case DiscordEventType.GuildUpdate:
                case DiscordEventType.GuildDelete:
                case DiscordEventType.GuildMemberAdd:
                case DiscordEventType.GuildMemberUpdate:
                case DiscordEventType.GuildMemberRemove:
                case DiscordEventType.GuildRoleCreate:
                case DiscordEventType.GuildRoleUpdate:
                case DiscordEventType.GuildRoleDelete:
                case DiscordEventType.InteractionCreate:
                case DiscordEventType.MessageCreate:
                case DiscordEventType.MessageReactionAdd:
                case DiscordEventType.MessageReactionRemove:
                case DiscordEventType.MessageUpdate:
                case DiscordEventType.MessageDelete:
                case DiscordEventType.MessageDeleteBulk:
                case DiscordEventType.MessageReactionRemoveAll:
                case DiscordEventType.MessageReactionRemoveEmoji:
                case DiscordEventType.StageInstanceCreate:
                case DiscordEventType.StageInstanceUpdate:
                case DiscordEventType.StageInstanceDelete:
                case DiscordEventType.ThreadCreate:
                case DiscordEventType.ThreadUpdate:
                case DiscordEventType.ThreadDelete:
                case DiscordEventType.ThreadListSync:
                case DiscordEventType.ThreadMemberUpdate:
                case DiscordEventType.ThreadMembersUpdate:
                    GatewayDispatchReceived?.Invoke(this, new DiscordGatewayDispatch(eventType.Value, payload.EventData!));
                    break;
                case DiscordEventType.Ready:
                    var readyDto = JsonSerializer.Deserialize<DiscordReadyDto>(payload.EventData!)!;
                    var ready = new DiscordReady(readyDto);
                    HandleReady(ready);
                    break;
                case DiscordEventType.Resumed:
                    _logger?.Debug("Gateway -- Resumed {Id} #{Number}", _session.Id, _session.SequenceNumber);
                    break;
                case DiscordEventType.UserUpdate:
                    // https://discord.com/developers/docs/topics/gateway#user-update
                    break;
                default:
                    _logger?.Error($"{typeof(DiscordEventType)} {eventType} is not supported");
                    break;
            }
        }

        private void HandleHeartbeatAck()
        {
            _session.HeartbeatAckReceived = true;
        }

        private async Task HandleHelloAsync(DiscordHelloDto helloDto, CancellationToken cancellationToken)
        {
            if (_session.HeartbeatLoopTask == null)
            {
                _session.HeartbeatLoopTask = Task.Run(
                    async () => await HeartbeatLoopAsync(TimeSpan.FromMilliseconds(helloDto.HeartbeatInterval)).ConfigureAwait(false),
                    cancellationToken);
            }
            else
            {
                _logger?.Error($"Gateway {nameof(HandleHelloAsync)} has already been received for this session.");
            }

            if (_session.Id != DiscordGatewaySession.EmptySessionId)
            {
                await SendResumeAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await SendIdentifyAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private async Task HandleInvalidSessionAsync(string eventDataJson, CancellationToken cancellationToken)
        {
            var waitSeconds = new Random().Next(1, 6);
            await Task.Delay(TimeSpan.FromSeconds(waitSeconds), cancellationToken).ConfigureAwait(false);

            var isResumable = eventDataJson.Equals("true", StringComparison.OrdinalIgnoreCase);
            if (isResumable)
            {
                await SendResumeAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                _session.Id = DiscordGatewaySession.EmptySessionId;
                _session.SequenceNumber = 0;
                await SendIdentifyAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private async Task HandlePayloadAsync(DiscordGatewayPayload payload, CancellationToken cancellationToken)
        {
            if (payload.Opcode != DiscordGatewayPayloadOpcode.Dispatch)
            {
                _logger?.Debug("Gateway << {Type}", payload.Opcode);
            }

            switch (payload.Opcode)
            {
                case DiscordGatewayPayloadOpcode.Dispatch:
                    HandleDispatch(payload);
                    break;
                case DiscordGatewayPayloadOpcode.Heartbeat:
                    await SendHeartbeatAsync(cancellationToken).ConfigureAwait(false);
                    break;
                case DiscordGatewayPayloadOpcode.Reconnect:
                    Reconnect(DiscordGatewayCloseType.UnknownError);
                    break;
                case DiscordGatewayPayloadOpcode.InvalidSession:
                    await HandleInvalidSessionAsync(payload.EventData!, cancellationToken).ConfigureAwait(false);
                    break;
                case DiscordGatewayPayloadOpcode.Hello:
                    var helloDto = JsonSerializer.Deserialize<DiscordHelloDto>(payload.EventData!)!;
                    await HandleHelloAsync(helloDto, cancellationToken).ConfigureAwait(false);
                    break;
                case DiscordGatewayPayloadOpcode.HeartbeatAck:
                    HandleHeartbeatAck();
                    break;
                default:
                    _logger?.Error($"{typeof(DiscordGatewayPayloadOpcode)} {payload.Opcode} is not supported");
                    break;
            }
        }

        private async Task HandlePayloadLoopException(Exception loopException)
        {
            if (_session.Status == DiscordGatewaySessionStatus.Disconnecting)
            {
                return;
            }

            var faultingSession = _session;
            await DisconnectAsync(DiscordGatewayCloseType.InternalServerError).ConfigureAwait(false);

            switch (loopException)
            {
                case DiscordGatewayClosedException closedException:
                    await HandleDiscordGatewayClosedExceptionAsync(faultingSession, closedException).ConfigureAwait(false);
                    break;

                case WebSocketException webSocketException:
                    await HandleWebSocketExceptionAsync(faultingSession, webSocketException).ConfigureAwait(false);
                    break;

                case OperationCanceledException:
                    _logger?.Debug($"{nameof(PayloadLoopAsync)} was cancelled.");
                    break;

                // ReSharper disable once ConvertTypeCheckPatternToNullCheck
                case Exception exception:
                    _logger?.Debug("Gateway -- Unhandled Exception ({Type})", exception.GetType().Name);
                    var gatewayException = new DiscordGatewayException("Unhandled exception", exception);
                    GatewayException?.Invoke(this, gatewayException);
                    break;
            }
        }

        private void HandleReady(DiscordReady ready)
        {
            _logger?.Debug("Gateway -- Started {Id}", ready.SessionId);

            _session.Id = ready.SessionId;
            _session.SequenceNumber = 0;

            GatewayReady?.Invoke(this, ready);
        }

        private async Task HandleWebSocketExceptionAsync(DiscordGatewaySession session, WebSocketException webSocketException)
        {
            _logger?.Debug(
                "Gateway -- WebSocketException ({Websocket}/{Error}/{Native})",
                webSocketException.WebSocketErrorCode,
                webSocketException.ErrorCode,
                webSocketException.NativeErrorCode);

            await ConnectUntilSuccessfulAsync(session.GatewayUrl, session.Intents, session.Id, session.SequenceNumber, session.Shard)
                .ConfigureAwait(false);
        }

        private DiscordEventType? ParseEventType(string? eventName)
        {
            if (eventName == null)
            {
                return null;
            }

            var eventTypeValue = EnglishTextInfo.ToTitleCase(eventName.ToLowerInvariant().Replace('_', ' ')).Replace(" ", "");

            if (Enum.TryParse<DiscordEventType>(eventTypeValue, true, out var eventType))
            {
                _logger?.Debug("Gateway << {Type}", eventType);
                return eventType;
            }

            _logger?.Debug("Gateway << {Type}", eventTypeValue);
            _logger?.Error($"{typeof(DiscordEventType)} {eventTypeValue} is not defined");
            return null;
        }

        private bool ShouldConnectAgain(Exception exception)
        {
            var exceptions = new List<Exception> { exception };
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                exceptions.Add(exception);
            }

            var socketException = (SocketException?)exceptions.FirstOrDefault(e => e is SocketException);
            var webSocketException = (WebSocketException?)exceptions.FirstOrDefault(e => e is WebSocketException);
            return ShouldConnectAgain(socketException) || ShouldConnectAgain(webSocketException);
        }

        private bool ShouldConnectAgain(WebSocketException? webSocketException)
        {
            if (webSocketException == null)
            {
                return false;
            }

            _logger?.Debug(webSocketException.Message);

            switch (webSocketException.WebSocketErrorCode)
            {
                case WebSocketError.InvalidMessageType:
                case WebSocketError.Faulted:
                case WebSocketError.NativeError:
                case WebSocketError.HeaderError:
                case WebSocketError.ConnectionClosedPrematurely:
                case WebSocketError.InvalidState:
                    _logger?.Debug("Attempt to connect again (reason: {Reason})", webSocketException.WebSocketErrorCode);
                    return true;
                default:
                    _logger?.Debug("Will not connect again (reason: {Reason})", webSocketException.WebSocketErrorCode);
                    return false;
            }
        }

        private bool ShouldConnectAgain(SocketException? socketException)
        {
            if (socketException == null)
            {
                return false;
            }

            _logger?.Debug(socketException.Message);

            switch (socketException.SocketErrorCode)
            {
                case SocketError.ConnectionAborted:
                case SocketError.ConnectionReset:
                case SocketError.ConnectionRefused:
                case SocketError.HostDown:
                case SocketError.HostUnreachable:
                case SocketError.HostNotFound:
                case SocketError.TryAgain:
                    _logger?.Debug("Attempt to connect again (reason: {Reason})", socketException.SocketErrorCode);
                    return true;
                default:
                    _logger?.Debug("Will not connect again (reason: {Reason})", socketException.SocketErrorCode);
                    return false;
            }
        }

        private bool ShouldReconnect(DiscordGatewayCloseType? closeType)
        {
            if (closeType == null)
            {
                return true;
            }

            switch (closeType)
            {
                case DiscordGatewayCloseType.EndpointUnavailable:
                case DiscordGatewayCloseType.Empty:
                case DiscordGatewayCloseType.InternalServerError:
                case DiscordGatewayCloseType.InvalidMessageType:
                case DiscordGatewayCloseType.InvalidPayloadData:
                case DiscordGatewayCloseType.MandatoryExtension:
                case DiscordGatewayCloseType.MessageTooBig:
                case DiscordGatewayCloseType.NormalClosure:
                case DiscordGatewayCloseType.PolicyViolation:
                case DiscordGatewayCloseType.ProtocolError:
                    return true;

                case DiscordGatewayCloseType.UnknownError:
                case DiscordGatewayCloseType.UnknownOpcode:
                case DiscordGatewayCloseType.DecodeError:
                case DiscordGatewayCloseType.NotAuthenticated:
                case DiscordGatewayCloseType.AlreadyAuthenticated:
                case DiscordGatewayCloseType.InvalidSeq:
                case DiscordGatewayCloseType.RateLimited:
                case DiscordGatewayCloseType.SessionTimedOut:
                    return true;

                case DiscordGatewayCloseType.AuthenticationFailed:
                case DiscordGatewayCloseType.InvalidShard:
                case DiscordGatewayCloseType.ShardingRequired:
                case DiscordGatewayCloseType.InvalidApiVersion:
                case DiscordGatewayCloseType.InvalidIntent:
                case DiscordGatewayCloseType.DisallowedIntent:
                    return false;

                default:
                    _logger?.Error($"{typeof(DiscordGatewayCloseType)} {closeType} is not supported");
                    return false;
            }
        }
    }
}