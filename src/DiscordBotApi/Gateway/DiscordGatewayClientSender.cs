// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayClientSender.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Gateway
{
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Gateway;
    using DiscordBotApi.Models.Gateway.Commands;

    internal partial class DiscordGatewayClient
    {
        private static string GetOperatingSystemName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return $"Windows {Environment.OSVersion.Version.Major}";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "Linux";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return "OSX";
            }

            return "Other";
        }

        private async Task SendHeartbeatAsync(CancellationToken cancellationToken)
        {
            _session.HeartbeatAckReceived = false;

            _logger?.Debug("Gateway >> {Name}", "Heartbeat");

            var payload = new DiscordGatewayPayload(DiscordGatewayPayloadOpcode.Heartbeat) { SequenceNumber = _session.SequenceNumber };
            await SendPayloadAsync(payload, cancellationToken).ConfigureAwait(false);
        }

        private async Task SendIdentifyAsync(CancellationToken cancellationToken)
        {
            var properties = new DiscordGatewayConnectionProperties(GetOperatingSystemName(), nameof(DiscordBotApi), nameof(DiscordBotApi));
            var identify = new DiscordIdentify(_botToken, properties, _session.Shard, (int)_session.Intents);
            var identifyDto = new DiscordIdentifyDto(identify);
            var identifyJson = JsonSerializer.Serialize(
                identifyDto,
                new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

            _logger?.Debug("Gateway >> {Name}", "Identify");

            var payload = new DiscordGatewayPayload(DiscordGatewayPayloadOpcode.Identify) { EventData = identifyJson };
            await SendPayloadAsync(payload, cancellationToken).ConfigureAwait(false);
        }

        private async Task SendPayloadAsync(DiscordGatewayPayload payload, CancellationToken cancellationToken)
        {
            var payloadDto = new DiscordGatewayPayloadDto(payload);
            var payloadJson = JsonSerializer.Serialize(payloadDto);
            var payloadBytes = Encoding.UTF8.GetBytes(payloadJson);

            await _session.WebSocket.SendAsync(payloadBytes, OutgoingWebSocketChunkLength, cancellationToken).ConfigureAwait(false);
        }

        private async Task SendResumeAsync(CancellationToken cancellationToken)
        {
            var resumeDto = new DiscordResumeDto(_botToken, _session.Id, _session.SequenceNumber);
            var resumeJson = JsonSerializer.Serialize(resumeDto);

            _logger?.Debug("Gateway >> {Name}", "Resume");

            var payload = new DiscordGatewayPayload(DiscordGatewayPayloadOpcode.Resume) { EventData = resumeJson };
            await SendPayloadAsync(payload, cancellationToken).ConfigureAwait(false);
        }

        private async Task SendPresenceUpdateAsync(DiscordPresenceUpdate presenceUpdate)
        {
            var presenceUpdateDto = new DiscordPresenceUpdateDto(presenceUpdate);
            var presenceUpdateJson = JsonSerializer.Serialize(presenceUpdateDto);

            _logger?.Debug("Gateway >> {Name}", "PresenceUpdate");

            var payload = new DiscordGatewayPayload(DiscordGatewayPayloadOpcode.PresenceUpdate) { EventData = presenceUpdateJson };
            await SendPayloadAsync(payload, CancellationToken.None).ConfigureAwait(false);
        }
    }
}