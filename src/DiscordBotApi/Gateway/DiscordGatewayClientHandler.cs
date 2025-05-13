// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayClientHandler.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text.Json;

using DiscordBotApi.Interface.Gateway;
using DiscordBotApi.Interface.Models.Gateway;
using DiscordBotApi.Interface.Models.Gateway.Commands;
using DiscordBotApi.Interface.Models.Gateway.Events;
using DiscordBotApi.Models.Gateway;
using DiscordBotApi.Models.Gateway.Events;

namespace DiscordBotApi.Gateway;

internal partial class DiscordGatewayClient
{
	private const int ConnectRetryIntervalSeconds = 2;
	private const int MaxConnectAttempts = 60;

	private async Task ConnectUntilSuccessfulAsync(
		string gatewayUrl,
		DiscordGatewayIntent intents,
		string? sessionId,
		int? sessionSequenceNumber,
		DiscordShard? shard
	)
	{
		var connectAttempts = 0;
		while (!_isDisposed)
		{
			try
			{
				_logger?.Debug(messageTemplate: "Attempt {Index} to connect", propertyValue: connectAttempts + 1);
				await ConnectAsync(
						gatewayUrl: gatewayUrl,
						intents: intents,
						sessionId: sessionId,
						sessionSequenceNumber: sessionSequenceNumber,
						shard: shard
					)
					.ConfigureAwait(continueOnCapturedContext: false);
				return;
			}
			catch (Exception exception) when (ShouldConnectAgain(exception: exception))
			{
				if (++connectAttempts >= MaxConnectAttempts)
				{
					GatewayException?.Invoke(
						sender: this,
						e: new DiscordGatewayException(
							message: $"{nameof(ConnectAsync)} failed",
							isDisconnected: true,
							innerException: exception
						)
					);
					return;
				}

				await Task
					.Delay(delay: TimeSpan.FromSeconds(value: ConnectRetryIntervalSeconds))
					.ConfigureAwait(continueOnCapturedContext: false);
			}
			catch (Exception exception)
			{
				GatewayException?.Invoke(
					sender: this,
					e: new DiscordGatewayException(
						message: $"{nameof(ConnectAsync)} failed",
						isDisconnected: true,
						innerException: exception
					)
				);
				return;
			}
		}
	}

	private async Task HandleDiscordGatewayClosedExceptionAsync(
		DiscordGatewaySession session,
		DiscordGatewayClosedException closedException
	)
	{
		_logger?.Debug(
			messageTemplate: "Gateway -- Disconnected ({CloseType})",
			propertyValue: closedException.CloseType
		);
		if (ShouldReconnect(closeType: closedException.CloseType))
		{
			_logger?.Debug(
				messageTemplate: "Attempt to reconnect (reason: {Reason})",
				propertyValue: closedException.CloseType
			);

			await ConnectUntilSuccessfulAsync(
					gatewayUrl: session.GatewayUrl,
					intents: session.Intents,
					sessionId: session.Id,
					sessionSequenceNumber: session.SequenceNumber,
					shard: session.Shard
				)
				.ConfigureAwait(continueOnCapturedContext: false);
		}
		else
		{
			_logger?.Debug(
				messageTemplate: "Will not reconnect (reason: {Reason})",
				propertyValue: closedException.CloseType
			);

			GatewayException?.Invoke(
				sender: this,
				e: new DiscordGatewayException(
					message: $"Disconnected: {closedException.CloseType} ({closedException.CloseStatusDescription})",
					isDisconnected: true
				)
			);
		}
	}

	private void HandleDispatch(DiscordGatewayPayload payload)
	{
		var eventType = ParseEventType(eventName: payload.EventName);
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
				try
				{
					GatewayDispatchReceived?.Invoke(
						sender: this,
						e: new DiscordGatewayDispatch(EventType: eventType.Value, EventDataJson: payload.EventData!)
					);
				}
				catch (Exception exception)
				{
					GatewayException?.Invoke(
						sender: this,
						e: new DiscordGatewayException(
							message: $"Failed to dispatch {eventType}",
							isDisconnected: false,
							innerException: exception
						)
					);
				}

				break;
			case DiscordEventType.Ready:
				var readyDto = JsonSerializer.Deserialize<DiscordReadyDto>(json: payload.EventData!)!;
				var ready = readyDto.ToModel();
				HandleReady(ready: ready);
				break;
			case DiscordEventType.Resumed:
				_logger?.Debug(
					messageTemplate: "Gateway -- Resumed {Id} #{Number}",
					propertyValue0: _session.Id,
					propertyValue1: _session.SequenceNumber
				);
				break;
			case DiscordEventType.UserUpdate:
				// https://discord.com/developers/docs/topics/gateway#user-update
				break;
			default:
				_logger?.Error(messageTemplate: $"{typeof(DiscordEventType)} {eventType} is not supported");
				break;
		}
	}

	private void HandleHeartbeatAck() => _session.HeartbeatAckReceived = true;

	private async Task HandleHelloAsync(DiscordHelloDto helloDto, CancellationToken cancellationToken)
	{
		if (_session.HeartbeatLoopTask == null)
		{
			_session.HeartbeatLoopTask = Task.Run(
				function: async () =>
					await HeartbeatLoopAsync(interval: TimeSpan.FromMilliseconds(value: helloDto.HeartbeatInterval))
						.ConfigureAwait(continueOnCapturedContext: false),
				cancellationToken: cancellationToken
			);
		}
		else
		{
			_logger?.Error(
				messageTemplate: $"Gateway {nameof(HandleHelloAsync)} has already been received for this session."
			);
		}

		if (_session.Id != DiscordGatewaySession.EmptySessionId)
		{
			await SendResumeAsync(cancellationToken: cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}
		else
		{
			await SendIdentifyAsync(cancellationToken: cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}
	}

	private async Task HandleInvalidSessionAsync(string eventDataJson, CancellationToken cancellationToken)
	{
		var waitSeconds = new Random().Next(minValue: 1, maxValue: 6);
		await Task
			.Delay(delay: TimeSpan.FromSeconds(value: waitSeconds), cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var isResumable = eventDataJson.Equals(value: "true", comparisonType: StringComparison.OrdinalIgnoreCase);
		if (isResumable)
		{
			await SendResumeAsync(cancellationToken: cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}
		else
		{
			_session.Id = DiscordGatewaySession.EmptySessionId;
			_session.SequenceNumber = 0;
			await SendIdentifyAsync(cancellationToken: cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		}
	}

	private async Task HandlePayloadAsync(DiscordGatewayPayload payload, CancellationToken cancellationToken)
	{
		if (payload.Opcode != DiscordGatewayPayloadOpcode.Dispatch)
		{
			_logger?.Debug(messageTemplate: "Client << Gateway -- {Type}", propertyValue: payload.Opcode);
		}

		switch (payload.Opcode)
		{
			case DiscordGatewayPayloadOpcode.Dispatch:
				HandleDispatch(payload: payload);
				break;
			case DiscordGatewayPayloadOpcode.Heartbeat:
				await SendHeartbeatAsync(cancellationToken: cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
				break;
			case DiscordGatewayPayloadOpcode.Reconnect:
				Reconnect(closeType: DiscordGatewayCloseType.UnknownError);
				break;
			case DiscordGatewayPayloadOpcode.InvalidSession:
				await HandleInvalidSessionAsync(eventDataJson: payload.EventData!, cancellationToken: cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
				break;
			case DiscordGatewayPayloadOpcode.Hello:
				var helloDto = JsonSerializer.Deserialize<DiscordHelloDto>(json: payload.EventData!)!;
				await HandleHelloAsync(helloDto: helloDto, cancellationToken: cancellationToken)
					.ConfigureAwait(continueOnCapturedContext: false);
				break;
			case DiscordGatewayPayloadOpcode.HeartbeatAck:
				HandleHeartbeatAck();
				break;
			default:
				_logger?.Error(messageTemplate: $"{typeof(DiscordGatewayPayloadOpcode)} {payload.Opcode} is not supported");
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
		await DisconnectAsync(closeType: DiscordGatewayCloseType.InternalServerError)
			.ConfigureAwait(continueOnCapturedContext: false);

		switch (loopException)
		{
			case DiscordGatewayClosedException closedException:
				await HandleDiscordGatewayClosedExceptionAsync(session: faultingSession, closedException: closedException)
					.ConfigureAwait(continueOnCapturedContext: false);
				break;

			case WebSocketException webSocketException:
				await HandleWebSocketExceptionAsync(session: faultingSession, webSocketException: webSocketException)
					.ConfigureAwait(continueOnCapturedContext: false);
				break;

			case OperationCanceledException:
				_logger?.Debug(messageTemplate: $"{nameof(PayloadLoopAsync)} was cancelled.");
				break;

			// ReSharper disable once ConvertTypeCheckPatternToNullCheck
			case Exception exception:
				_logger?.Debug(
					messageTemplate: "Gateway -- Unhandled Exception ({Type})",
					propertyValue: exception.GetType().Name
				);
				var gatewayException = new DiscordGatewayException(
					message: "Unhandled exception",
					isDisconnected: true,
					innerException: exception
				);
				GatewayException?.Invoke(sender: this, e: gatewayException);
				break;
		}
	}

	private void HandleReady(DiscordReady ready)
	{
		_logger?.Debug(messageTemplate: "Gateway -- Started {Id}", propertyValue: ready.SessionId);

		_session.Id = ready.SessionId;
		_session.SequenceNumber = 0;

		GatewayReady?.Invoke(sender: this, e: ready);
	}

	private async Task HandleWebSocketExceptionAsync(
		DiscordGatewaySession session,
		WebSocketException webSocketException
	)
	{
		_logger?.Debug(
			messageTemplate: "Gateway -- WebSocketException ({Websocket}/{Error}/{Native})",
			propertyValue0: webSocketException.WebSocketErrorCode,
			propertyValue1: webSocketException.ErrorCode,
			propertyValue2: webSocketException.NativeErrorCode
		);

		await ConnectUntilSuccessfulAsync(
				gatewayUrl: session.GatewayUrl,
				intents: session.Intents,
				sessionId: session.Id,
				sessionSequenceNumber: session.SequenceNumber,
				shard: session.Shard
			)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	private DiscordEventType? ParseEventType(string? eventName)
	{
		if (eventName == null)
		{
			return null;
		}

		var eventTypeValue = EnglishTextInfo
			.ToTitleCase(str: eventName.ToLowerInvariant().Replace(oldChar: '_', newChar: ' '))
			.Replace(oldValue: " ", newValue: "");

		if (Enum.TryParse<DiscordEventType>(value: eventTypeValue, ignoreCase: true, result: out var eventType))
		{
			_logger?.Debug(messageTemplate: "Client << Gateway -- {Type}", propertyValue: eventType);
			return eventType;
		}

		_logger?.Debug(messageTemplate: "Client << Gateway -- {Type}", propertyValue: eventTypeValue);
		_logger?.Error(messageTemplate: $"{typeof(DiscordEventType)} {eventTypeValue} is not defined");
		return null;
	}

	private bool ShouldConnectAgain(Exception exception)
	{
		var exceptions = new List<Exception> { exception };
		while (exception.InnerException != null)
		{
			exception = exception.InnerException;
			exceptions.Add(item: exception);
		}

		var socketException = (SocketException?)exceptions.FirstOrDefault(predicate: e => e is SocketException);
		var webSocketException = (WebSocketException?)exceptions.FirstOrDefault(predicate: e => e is WebSocketException);
		return ShouldConnectAgain(socketException: socketException) ||
			ShouldConnectAgain(webSocketException: webSocketException);
	}

	private bool ShouldConnectAgain(WebSocketException? webSocketException)
	{
		if (webSocketException == null)
		{
			return false;
		}

		_logger?.Debug(messageTemplate: webSocketException.Message);

		switch (webSocketException.WebSocketErrorCode)
		{
			case WebSocketError.InvalidMessageType:
			case WebSocketError.Faulted:
			case WebSocketError.NativeError:
			case WebSocketError.HeaderError:
			case WebSocketError.ConnectionClosedPrematurely:
			case WebSocketError.InvalidState:
				_logger?.Debug(
					messageTemplate: "Attempt to connect again (reason: {Reason})",
					propertyValue: webSocketException.WebSocketErrorCode
				);
				return true;
			default:
				_logger?.Debug(
					messageTemplate: "Will not connect again (reason: {Reason})",
					propertyValue: webSocketException.WebSocketErrorCode
				);
				return false;
		}
	}

	private bool ShouldConnectAgain(SocketException? socketException)
	{
		if (socketException == null)
		{
			return false;
		}

		_logger?.Debug(messageTemplate: socketException.Message);

		switch (socketException.SocketErrorCode)
		{
			case SocketError.ConnectionAborted:
			case SocketError.ConnectionReset:
			case SocketError.ConnectionRefused:
			case SocketError.HostDown:
			case SocketError.HostUnreachable:
			case SocketError.HostNotFound:
			case SocketError.TryAgain:
				_logger?.Debug(
					messageTemplate: "Attempt to connect again (reason: {Reason})",
					propertyValue: socketException.SocketErrorCode
				);
				return true;
			default:
				_logger?.Debug(
					messageTemplate: "Will not connect again (reason: {Reason})",
					propertyValue: socketException.SocketErrorCode
				);
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
				_logger?.Error(messageTemplate: $"{typeof(DiscordGatewayCloseType)} {closeType} is not supported");
				return false;
		}
	}
}