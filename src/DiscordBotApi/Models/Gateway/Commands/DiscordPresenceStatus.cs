// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPresenceStatus.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands;

public enum DiscordPresenceStatus
{
	Online,
	DoNotDisturb,
	Idle,
	Invisible,
	Offline
}