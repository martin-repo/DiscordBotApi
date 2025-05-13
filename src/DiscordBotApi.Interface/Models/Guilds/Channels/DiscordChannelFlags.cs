// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordChannelFlags.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#channel-object-channel-flags
[Flags]
public enum DiscordChannelFlags
{
	None = 0,
	Pinned = 1 << 1,
	RequireTag = 1 << 4,
	HideMediaDownloadOptions = 1 << 15
}