﻿// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGatewayPayloadOpcode.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway;

// https://discord.com/developers/docs/topics/opcodes-and-status-codes#gateway-gateway-opcodes
internal enum DiscordGatewayPayloadOpcode
{
	Dispatch = 0,
	Heartbeat = 1,
	Identify = 2,
	PresenceUpdate = 3,
	VoiceStateUpdate = 4,
	Resume = 6,
	Reconnect = 7,
	RequestGuildMembers = 8,
	InvalidSession = 9,
	Hello = 10,
	HeartbeatAck = 11
}