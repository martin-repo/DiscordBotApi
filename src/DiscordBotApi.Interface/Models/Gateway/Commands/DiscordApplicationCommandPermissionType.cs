// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandPermissionType.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Gateway.Commands;

// https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-application-command-permission-type
public enum DiscordApplicationCommandPermissionType
{
	Role = 1,
	User = 2,
	Channel = 3
}