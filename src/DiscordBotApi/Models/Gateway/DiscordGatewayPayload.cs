// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayPayload.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway
{
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
}