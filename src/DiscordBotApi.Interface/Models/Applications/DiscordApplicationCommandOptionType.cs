// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOptionType.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Applications;

// https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-type
public enum DiscordApplicationCommandOptionType
{
	SubCommand = 1,
	SubCommandGroup = 2,
	String = 3,
	Integer = 4,
	Boolean = 5,
	User = 6,
	Channel = 7,
	Role = 8,
	Mentionable = 9,
	Number = 10,
	Attachment = 11
}