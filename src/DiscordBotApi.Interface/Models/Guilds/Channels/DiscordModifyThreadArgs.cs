// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyThreadArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels;

public sealed class DiscordModifyThreadArgs
{
	public bool? Archived { get; init; }

	public DiscordChannelFlags? Flags { get; init; }

	public string? Name { get; init; }
}