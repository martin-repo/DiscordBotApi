// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordChannelFlags.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#channel-object-channel-flags
[Flags]
public enum DiscordChannelFlags
{
	None = 0,
	Pinned = 1 << 1,
	RequireTag = 1 << 4,
	HideMediaDownloadOptions = 1 << 15
}