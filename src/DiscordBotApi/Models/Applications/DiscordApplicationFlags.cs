﻿// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationFlags.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications;

[Flags]
public enum DiscordApplicationFlags : uint
{
	None = 0,
	GatewayPresence = 1 << 12,
	GatewayPresenceLimited = 1 << 13,
	GatewayGuildMembers = 1 << 14,
	GatewayGuildMembersLimited = 1 << 15,
	VerificationPendingGuildLimit = 1 << 16,
	Embedded = 1 << 17,
	GatewayMessageContent = 1 << 18,
	GatewayMessageContentLimited = 1 << 19
}