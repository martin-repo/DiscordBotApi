﻿// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayPayload.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway;

internal record DiscordGatewayPayload
{
	internal DiscordGatewayPayload(DiscordGatewayPayloadDto dto)
	{
		Opcode = (DiscordGatewayPayloadOpcode)dto.Opcode;
		SequenceNumber = dto.SequenceNumber;
		EventName = dto.EventName;
		EventData = dto.EventData?.Json;
	}

	internal DiscordGatewayPayload(DiscordGatewayPayloadOpcode opcode)
	{
		Opcode = opcode;
	}

	public string? EventData { get; init; }

	public string? EventName { get; init; }

	public DiscordGatewayPayloadOpcode Opcode { get; init; }

	public int? SequenceNumber { get; init; }
}