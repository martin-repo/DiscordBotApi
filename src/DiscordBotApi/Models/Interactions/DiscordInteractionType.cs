// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionType.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions;

public enum DiscordInteractionType
{
	Ping = 1,
	ApplicationCommand = 2,
	MessageComponent = 3,
	ApplicaitonCommandAutocomplete = 4,
	ModalSubmit = 5
}