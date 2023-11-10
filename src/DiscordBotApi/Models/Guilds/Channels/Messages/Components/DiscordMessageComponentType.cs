// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageComponentType.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components;

// https://discord.com/developers/docs/interactions/message-components#component-object-component-types
public enum DiscordMessageComponentType
{
	ActionRow = 1,
	Button = 2,
	SelectMenu = 3,
	TextInput = 4
}