// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationFlags.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Applications;

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