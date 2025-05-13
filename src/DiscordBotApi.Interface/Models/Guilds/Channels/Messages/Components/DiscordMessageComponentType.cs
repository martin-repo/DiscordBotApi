// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageComponentType.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

// https://discord.com/developers/docs/interactions/message-components#component-object-component-types
public enum DiscordMessageComponentType
{
	ActionRow = 1,
	Button = 2,
	SelectMenu = 3,
	TextInput = 4
}