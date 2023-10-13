// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateDmArgs.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

public record DiscordCreateDmArgs
{
	public ulong RecipientId { get; init; }
}