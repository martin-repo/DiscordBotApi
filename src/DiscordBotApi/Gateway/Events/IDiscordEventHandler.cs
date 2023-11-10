// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordEventHandler.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;

using DiscordBotApi.Models.Applications;
using DiscordBotApi.Models.Guilds;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Interactions;

namespace DiscordBotApi.Gateway.Events;

public interface IDiscordEventHandler
{
	IEnumerable<DiscordEventType> EventTypes { get; }

	Task<bool> HandleGuildMemberAddAsync(DiscordGuildMember member);

	Task<bool> HandleInteractionCreateAsync(DiscordInteraction interaction);

	Task<bool> HandleMessageCreateAsync(DiscordMessage message);

	Task InitializeAsync(
		ImmutableArray<DiscordApplicationCommand> currentGlobalCommands,
		ImmutableArray<DiscordApplicationCommand>? currentGuildCommands
	);
}