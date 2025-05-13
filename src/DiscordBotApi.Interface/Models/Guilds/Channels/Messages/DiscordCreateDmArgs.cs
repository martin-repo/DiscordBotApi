// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateDmArgs.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public sealed class DiscordCreateDmArgs
{
	public required ulong RecipientId { get; init; }
}