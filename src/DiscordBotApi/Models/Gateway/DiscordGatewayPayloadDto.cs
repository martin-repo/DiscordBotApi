// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayPayloadDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace DiscordBotApi.Models.Gateway;

internal sealed record DiscordGatewayPayloadDto(
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
	public static DiscordGatewayPayloadDto FromModel(DiscordGatewayPayload model) =>
		new(
			Opcode: (int)model.Opcode,
			SequenceNumber: model.SequenceNumber,
			EventName: model.EventName,
			EventData: model.EventData != null ? new JsonData(Json: model.EventData) : null
		);

	public DiscordGatewayPayload ToModel() =>
		new()
		{
			Opcode = (DiscordGatewayPayloadOpcode)Opcode,
			SequenceNumber = SequenceNumber,
			EventName = EventName,
			EventData = EventData?.Json
		};
}