// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewaySession.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Gateway;
using DiscordBotApi.Models.Gateway.Commands;

namespace DiscordBotApi.Gateway;

internal class DiscordGatewaySession(
	IBinaryWebSocket webSocket,
	IZlibContext zlibContext,
	string gatewayUrl,
	DiscordGatewayIntent intents,
	DiscordShard? shard
)
{
	public const string EmptySessionId = "";

	public string GatewayUrl { get; } = gatewayUrl;

	public bool HeartbeatAckReceived { get; set; } = true;

	public CancellationTokenSource HeartbeatCancellationSource { get; } = new();

	public Task? HeartbeatLoopTask { get; set; }

	public string Id { get; set; } = EmptySessionId;

	public DiscordGatewayIntent Intents { get; } = intents;

	public CancellationTokenSource PayloadCancellationSource { get; } = new();

	public Task? PayloadLoopTask { get; set; }

	public int SequenceNumber { get; set; }

	public DiscordShard? Shard { get; } = shard;

	public DiscordGatewaySessionStatus Status { get; set; } = DiscordGatewaySessionStatus.Connected;

	public IBinaryWebSocket WebSocket { get; } = webSocket;

	public IZlibContext ZlibContext { get; } = zlibContext;
}