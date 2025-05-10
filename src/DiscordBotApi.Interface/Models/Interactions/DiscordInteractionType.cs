// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionType.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Interface.Models.Interactions;

public enum DiscordInteractionType
{
	Ping = 1,
	ApplicationCommand = 2,
	MessageComponent = 3,
	ApplicationCommandAutocomplete = 4,
	ModalSubmit = 5
}