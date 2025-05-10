// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordField.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;

public sealed class DiscordField
{
	public bool? Inline { get; init; }

	public required string Name { get; init; }

	public required string Value { get; init; }
}