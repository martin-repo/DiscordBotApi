// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayPayload.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway;

internal sealed record DiscordGatewayPayload
{
	public string? EventData { get; init; }

	public string? EventName { get; init; }

	public DiscordGatewayPayloadOpcode Opcode { get; init; }

	public int? SequenceNumber { get; init; }
}