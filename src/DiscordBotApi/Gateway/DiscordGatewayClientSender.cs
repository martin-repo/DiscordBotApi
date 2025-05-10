// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayClientSender.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using DiscordBotApi.Models.Gateway;
using DiscordBotApi.Models.Gateway.Commands;

namespace DiscordBotApi.Gateway;

internal partial class DiscordGatewayClient
{
	private static string GetOperatingSystemName()
	{
		if (RuntimeInformation.IsOSPlatform(osPlatform: OSPlatform.Windows))
		{
			return $"Windows {Environment.OSVersion.Version.Major}";
		}

		if (RuntimeInformation.IsOSPlatform(osPlatform: OSPlatform.Linux))
		{
			return "Linux";
		}

		if (RuntimeInformation.IsOSPlatform(osPlatform: OSPlatform.OSX))
		{
			return "OSX";
		}

		return "Other";
	}

	private async Task SendHeartbeatAsync(CancellationToken cancellationToken)
	{
		_session.HeartbeatAckReceived = false;

		_logger?.Debug(messageTemplate: "Gateway >> {Name}", propertyValue: "Heartbeat");

		var payload =
			new DiscordGatewayPayload(opcode: DiscordGatewayPayloadOpcode.Heartbeat) { SequenceNumber = _session.SequenceNumber };
		await SendPayloadAsync(payload: payload, cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	private async Task SendIdentifyAsync(CancellationToken cancellationToken)
	{
		var properties = new DiscordGatewayConnectionProperties(
			OperatingSystem: GetOperatingSystemName(),
			BrowserName: nameof(DiscordBotApi),
			DeviceName: nameof(DiscordBotApi));
		var identify = new DiscordIdentify(
			Token: botToken,
			Properties: properties,
			Shard: _session.Shard,
			Intents: (int)_session.Intents);
		var identifyDto = new DiscordIdentifyDto(model: identify);
		var identifyJson = JsonSerializer.Serialize(
			value: identifyDto,
			options: new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

		_logger?.Debug(messageTemplate: "Gateway >> {Name}", propertyValue: "Identify");

		var payload = new DiscordGatewayPayload(opcode: DiscordGatewayPayloadOpcode.Identify) { EventData = identifyJson };
		await SendPayloadAsync(payload: payload, cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	private async Task SendPayloadAsync(DiscordGatewayPayload payload, CancellationToken cancellationToken)
	{
		var payloadDto = new DiscordGatewayPayloadDto(model: payload);
		var payloadJson = JsonSerializer.Serialize(value: payloadDto);
		var payloadBytes = Encoding.UTF8.GetBytes(s: payloadJson);

		await _session.WebSocket.SendAsync(
				bytes: payloadBytes,
				chunkLength: OutgoingWebSocketChunkLength,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	private async Task SendPresenceUpdateAsync(DiscordPresenceUpdate presenceUpdate, CancellationToken cancellationToken)
	{
		var presenceUpdateDto = new DiscordPresenceUpdateDto(model: presenceUpdate);
		var presenceUpdateJson = JsonSerializer.Serialize(value: presenceUpdateDto);

		_logger?.Debug(messageTemplate: "Gateway >> {Name}", propertyValue: "PresenceUpdate");

		var payload =
			new DiscordGatewayPayload(opcode: DiscordGatewayPayloadOpcode.PresenceUpdate) { EventData = presenceUpdateJson };
		await SendPayloadAsync(payload: payload, cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	private async Task SendResumeAsync(CancellationToken cancellationToken)
	{
		var resumeDto = new DiscordResumeDto(Token: botToken, SessionId: _session.Id, SequenceNumber: _session.SequenceNumber);
		var resumeJson = JsonSerializer.Serialize(value: resumeDto);

		_logger?.Debug(messageTemplate: "Gateway >> {Name}", propertyValue: "Resume");

		var payload = new DiscordGatewayPayload(opcode: DiscordGatewayPayloadOpcode.Resume) { EventData = resumeJson };
		await SendPayloadAsync(payload: payload, cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}
}