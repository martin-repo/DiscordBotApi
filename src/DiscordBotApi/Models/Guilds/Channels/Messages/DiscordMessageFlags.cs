// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageFlags.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

[Flags]
public enum DiscordMessageFlags : ulong
{
	SuppressEmbeds = 1 << 2,
	Ephemeral = 1 << 6
}