// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOptionChoice.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Applications;

public sealed class DiscordApplicationCommandOptionChoice
{
	public required string Name { get; init; }

	public required object Value { get; init; }
}