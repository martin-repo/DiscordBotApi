// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayPayloadDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway;

internal record DiscordGatewayPayloadDto(
	[property: JsonPropertyName(name: "op")]
	int Opcode,
	[property: JsonPropertyName(name: "s")]
	int? SequenceNumber,
	[property: JsonPropertyName(name: "t")]
	string? EventName,
	[property: JsonPropertyName(name: "d")]
	JsonData? EventData
)
{
	internal DiscordGatewayPayloadDto(DiscordGatewayPayload model) : this(
		Opcode: (int)model.Opcode,
		SequenceNumber: model.SequenceNumber,
		EventName: model.EventName,
		EventData: model.EventData != null
			? new JsonData(Json: model.EventData)
			: null)
	{
	}
}