// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewaySession.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Gateway;
using DiscordBotApi.Models.Gateway.Commands;

namespace DiscordBotApi.Gateway;

internal class DiscordGatewaySession
{
	public const string EmptySessionId = "";

	public DiscordGatewaySession(
		IBinaryWebSocket webSocket,
		IZlibContext zlibContext,
		string gatewayUrl,
		DiscordGatewayIntent intents,
		DiscordShard? shard
	)
	{
		WebSocket = webSocket;
		ZlibContext = zlibContext;
		GatewayUrl = gatewayUrl;
		Intents = intents;
		Shard = shard;
		HeartbeatCancellationSource = new CancellationTokenSource();
		PayloadCancellationSource = new CancellationTokenSource();
	}

	public string GatewayUrl { get; }

	public bool HeartbeatAckReceived { get; set; } = true;

	public CancellationTokenSource HeartbeatCancellationSource { get; }

	public Task? HeartbeatLoopTask { get; set; }

	public string Id { get; set; } = EmptySessionId;

	public DiscordGatewayIntent Intents { get; }

	public CancellationTokenSource PayloadCancellationSource { get; }

	public Task? PayloadLoopTask { get; set; }

	public int SequenceNumber { get; set; }

	public DiscordShard? Shard { get; }

	public DiscordGatewaySessionStatus Status { get; set; } = DiscordGatewaySessionStatus.Connected;

	public IBinaryWebSocket WebSocket { get; }

	public IZlibContext ZlibContext { get; }
}