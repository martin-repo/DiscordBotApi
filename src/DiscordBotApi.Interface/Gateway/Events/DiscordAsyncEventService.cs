// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordAsyncEventService.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Interface.Models.Interactions;

namespace DiscordBotApi.Interface.Gateway.Events;

public interface IDiscordAsyncEventService
{
	public event Func<DiscordInteraction, Task> InteractionCreate;
	//
	// public delegate Task HandleGuildMemberAddAsync(DiscordGuildMemberAdd member);
	//
	// public delegate Task HandleInteractionAsync(DiscordInteraction interaction);
	//
	public event Func<DiscordMessage, Task> MessageCreate;
	//
	// public delegate Task HandleMessageDeleteAsync(DiscordMessageDelete message);
	//
	// public delegate Task HandleMessageReactionAddAsync(DiscordMessageReactionAdd messageReaction);
	//
	// public delegate Task HandleMessageReactionRemoveAsync(DiscordMessageReactionRemove messageReaction);
	//
	// public delegate Task HandleMessageUpdateAsync(DiscordUpdatedMessage message);
	//
	// void HandleGuildMemberAdd(HandleGuildMemberAddAsync handler);
	//
	// void HandleInteractionCreate(HandleInteractionAsync handler);
	//
	// void HandleMessageCreate(HandleMessageAsync handler);
	//
	// void HandleMessageDelete(HandleMessageDeleteAsync handler);
	//
	// void HandleMessageReactionAdd(HandleMessageReactionAddAsync handler);
	//
	// void HandleMessageReactionRemove(HandleMessageReactionRemoveAsync handler);
	//
	// void HandleMessageUpdate(HandleMessageUpdateAsync handler);
}