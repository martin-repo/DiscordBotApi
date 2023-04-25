// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyThreadArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels;

public record DiscordModifyThreadArgs
{
	public bool? Archived { get; init; }
}