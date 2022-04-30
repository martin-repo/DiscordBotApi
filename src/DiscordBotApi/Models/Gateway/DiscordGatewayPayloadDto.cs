// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayPayloadDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway
{
    using System.Text.Json.Serialization;

    internal record DiscordGatewayPayloadDto(
        [property: JsonPropertyName("op")] int Opcode,
        [property: JsonPropertyName("s")] int? SequenceNumber,
        [property: JsonPropertyName("t")] string? EventName,
        [property: JsonPropertyName("d")] JsonData? EventData)
    {
        internal DiscordGatewayPayloadDto(DiscordGatewayPayload model)
            : this((int)model.Opcode, model.SequenceNumber, model.EventName, model.EventData != null ? new JsonData(model.EventData) : null)
        {
        }
    }
}