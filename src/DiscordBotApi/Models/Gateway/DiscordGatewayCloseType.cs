// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayCloseType.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway
{
    public enum DiscordGatewayCloseType
    {
        // System.Net.WebSockets.WebSocketCloseStatus
        NormalClosure = 1000, // 0x000003E8
        EndpointUnavailable = 1001, // 0x000003E9
        ProtocolError = 1002, // 0x000003EA
        InvalidMessageType = 1003, // 0x000003EB
        Empty = 1005, // 0x000003ED
        InvalidPayloadData = 1007, // 0x000003EF
        PolicyViolation = 1008, // 0x000003F0
        MessageTooBig = 1009, // 0x000003F1
        MandatoryExtension = 1010, // 0x000003F2
        InternalServerError = 1011, // 0x000003F3

        // https://discord.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-close-event-codes
        UnknownError = 4000,
        UnknownOpcode = 4001,
        DecodeError = 4002,
        NotAuthenticated = 4003,
        AuthenticationFailed = 4004,
        AlreadyAuthenticated = 4005,
        InvalidSeq = 4007,
        RateLimited = 4008,
        SessionTimedOut = 4009,
        InvalidShard = 4010,
        ShardingRequired = 4011,
        InvalidApiVersion = 4012,
        InvalidIntent = 4013,
        DisallowedIntent = 4014
    }
}