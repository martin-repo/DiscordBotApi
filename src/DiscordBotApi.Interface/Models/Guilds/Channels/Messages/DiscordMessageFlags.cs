// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageFlags.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

[Flags]
public enum DiscordMessageFlags : uint
{
	Crossposted = 1 << 0,
	IsCrosspost = 1 << 1,
	SuppressEmbeds = 1 << 2,
	SourceMessageDeleted = 1 << 3,
	Urgent = 1 << 4,
	HasThread = 1 << 5,
	Ephemeral = 1 << 6,
	Loading = 1 << 7,
	FailedToMentionSomeRolesInThread = 1 << 8,
	SuppressNotifications = 1 << 12,
	IsVoiceMessage = 1 << 13
}