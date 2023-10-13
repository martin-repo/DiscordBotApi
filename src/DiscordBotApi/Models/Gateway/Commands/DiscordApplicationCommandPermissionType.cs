// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandPermissionType.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands;

// https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-application-command-permission-type
public enum DiscordApplicationCommandPermissionType
{
	Role = 1,
	User = 2,
	Channel = 3
}