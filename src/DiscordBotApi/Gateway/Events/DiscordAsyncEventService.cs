// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordAsyncEventService.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Concurrent;

using DiscordBotApi.Interface;
using DiscordBotApi.Interface.Gateway.Events;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Interface.Models.Interactions;
using DiscordBotApi.Models.Gateway.Events;

using Serilog;

namespace DiscordBotApi.Gateway.Events;

public class DiscordAsyncEventService : IDiscordAsyncEventService
{
	private readonly IDiscordBotClient _botClient;
	private readonly CancellationTokenSource _cancellationTokenSource = new();
	private readonly BlockingCollection<(DiscordEventType EventType, object EventData)> _gatewayEvents = new();
	private readonly EventHandlerManager<DiscordInteraction> _interactionCreateManager;
	private readonly EventHandlerManager<DiscordMessage> _messageCreateManager;
	private readonly Task _processingTask;

	public DiscordAsyncEventService(IDiscordBotClient botClient)
	{
		_botClient = botClient;

		_interactionCreateManager = new EventHandlerManager<DiscordInteraction>(
			gatewayEvents: _gatewayEvents,
			eventType: DiscordEventType.InteractionCreate,
			subscribe: handler => _botClient.InteractionCreate += handler,
			unsubscribe: handler => _botClient.InteractionCreate -= handler
		);

		_messageCreateManager = new EventHandlerManager<DiscordMessage>(
			gatewayEvents: _gatewayEvents,
			eventType: DiscordEventType.MessageCreate,
			subscribe: handler => _botClient.MessageCreate += handler,
			unsubscribe: handler => _botClient.MessageCreate -= handler
		);

		// TODO: Maybe add a method to stop/dispose if needed.
		_processingTask = Task
			.Factory.StartNew(
				function: async () => await ProcessGatewayEventsAsync(cancellationToken: _cancellationTokenSource.Token)
					.ConfigureAwait(continueOnCapturedContext: false),
				cancellationToken: _cancellationTokenSource.Token,
				creationOptions: TaskCreationOptions.LongRunning,
				scheduler: TaskScheduler.Default
			)
			.Unwrap();
	}

	public event Func<DiscordInteraction, Task> InteractionCreate
	{
		add => _interactionCreateManager.Add(handler: value);
		remove => _interactionCreateManager.Remove(handler: value);
	}

	public event Func<DiscordMessage, Task> MessageCreate
	{
		add => _messageCreateManager.Add(handler: value);
		remove => _messageCreateManager.Remove(handler: value);
	}

	// // TODO: Maybe add corresponding Unhandle... methods if that is needed.
	// public void HandleGuildMemberAdd(IDiscordAsyncEventService.HandleGuildMemberAddAsync handler) =>
	// 	AddHandler(
	// 		eventType: DiscordEventType.GuildMemberAdd,
	// 		subscribeAction: () => _botClient.GuildMemberAdd += OnGuildMemberAdd,
	// 		handleDelegate: handler
	// 	);
	//
	// public void HandleInteractionCreate(IDiscordAsyncEventService.HandleInteractionAsync handler) =>
	// 	AddHandler(
	// 		eventType: DiscordEventType.InteractionCreate,
	// 		subscribeAction: () => _botClient.InteractionCreate += OnInteractionCreate,
	// 		handleDelegate: handler
	// 	);
	//
	// public void HandleMessageCreate(IDiscordAsyncEventService.HandleMessageAsync handler) =>
	// 	AddHandler(
	// 		eventType: DiscordEventType.MessageCreate,
	// 		subscribeAction: () => _botClient.MessageCreate += OnMessageCreate,
	// 		handleDelegate: handler
	// 	);
	//
	// public void HandleMessageDelete(IDiscordAsyncEventService.HandleMessageDeleteAsync handler) =>
	// 	AddHandler(
	// 		eventType: DiscordEventType.MessageDelete,
	// 		subscribeAction: () => _botClient.MessageDelete += OnMessageDelete,
	// 		handleDelegate: handler
	// 	);
	//
	// public void HandleMessageReactionAdd(IDiscordAsyncEventService.HandleMessageReactionAddAsync handler) =>
	// 	AddHandler(
	// 		eventType: DiscordEventType.MessageReactionAdd,
	// 		subscribeAction: () => _botClient.MessageReactionAdd += OnMessageReactionAdd,
	// 		handleDelegate: handler
	// 	);
	//
	// public void HandleMessageReactionRemove(IDiscordAsyncEventService.HandleMessageReactionRemoveAsync handler) =>
	// 	AddHandler(
	// 		eventType: DiscordEventType.MessageReactionRemove,
	// 		subscribeAction: () => _botClient.MessageReactionRemove += OnMessageReactionRemove,
	// 		handleDelegate: handler
	// 	);
	//
	// public void HandleMessageUpdate(IDiscordAsyncEventService.HandleMessageUpdateAsync handler) =>
	// 	AddHandler(
	// 		eventType: DiscordEventType.MessageUpdate,
	// 		subscribeAction: () => _botClient.MessageUpdate += OnMessageUpdate,
	// 		handleDelegate: handler
	// 	);

	// private void AddHandler(DiscordEventType eventType, Action subscribeAction, object handleDelegate)
	// {
	// 	lock (_handledEventTypesLock)
	// 	{
	// 		if (!_handledEventTypes.Contains(item: eventType))
	// 		{
	// 			subscribeAction();
	// 			_handledEventTypes.Add(item: eventType);
	// 		}
	//
	// 		if (!_handlersByEventType.ContainsKey(key: eventType))
	// 		{
	// 			_handlersByEventType.Add(key: eventType, value: new List<object>());
	// 		}
	//
	// 		_handlersByEventType[key: eventType].Add(item: handleDelegate);
	// 	}
	// }

	// private void OnGuildMemberAdd(object? _, DiscordGuildMemberAdd member) =>
	// 	_gatewayEvents.Add(item: new DiscordGatewayEvent(EventType: DiscordEventType.GuildMemberAdd, EventData: member));
	//
	// private void OnInteractionCreate(object? _, DiscordInteraction interaction) =>
	// 	_gatewayEvents.Add(
	// 		item: new DiscordGatewayEvent(EventType: DiscordEventType.InteractionCreate, EventData: interaction)
	// 	);
	//
	// private void OnMessageCreate(object? _, DiscordMessage message) =>
	// 	_gatewayEvents.Add(item: new DiscordGatewayEvent(EventType: DiscordEventType.MessageCreate, EventData: message));
	//
	// private void OnMessageDelete(object? _, DiscordMessageDelete message) =>
	// 	_gatewayEvents.Add(item: new DiscordGatewayEvent(EventType: DiscordEventType.MessageCreate, EventData: message));
	//
	// private void OnMessageReactionAdd(object? _, DiscordMessageReactionAdd messageReaction) =>
	// 	_gatewayEvents.Add(
	// 		item: new DiscordGatewayEvent(EventType: DiscordEventType.MessageReactionAdd, EventData: messageReaction)
	// 	);
	//
	// private void OnMessageReactionRemove(object? _, DiscordMessageReactionRemove messageReaction) =>
	// 	_gatewayEvents.Add(
	// 		item: new DiscordGatewayEvent(EventType: DiscordEventType.MessageReactionRemove, EventData: messageReaction)
	// 	);
	//
	// private void OnMessageUpdate(object? _, DiscordUpdatedMessage message) =>
	// 	_gatewayEvents.Add(item: new DiscordGatewayEvent(EventType: DiscordEventType.MessageCreate, EventData: message));

	private async Task ProcessGatewayEventsAsync(CancellationToken cancellationToken)
	{
		try
		{
			foreach (var gatewayEvent in _gatewayEvents.GetConsumingEnumerable(cancellationToken: cancellationToken))
			{
				try
				{
					switch (gatewayEvent.EventType)
					{
						// case DiscordEventType.GuildMemberAdd:
						// 	await Task
						// 		.WhenAll(
						// 			tasks: _handlersByEventType[key: DiscordEventType.GuildMemberAdd]
						// 				.Cast<IDiscordAsyncEventService.HandleGuildMemberAddAsync>()
						// 				.Select(selector: v => v(member: (DiscordGuildMemberAdd)gatewayEvent.EventData))
						// 		)
						// 		.ConfigureAwait(continueOnCapturedContext: false);
						// 	break;

						case DiscordEventType.InteractionCreate:
							await _interactionCreateManager
								.InvokeAsync(args: (DiscordInteraction)gatewayEvent.EventData)
								.ConfigureAwait(continueOnCapturedContext: false);
							break;
						case DiscordEventType.MessageCreate:
							await _messageCreateManager
								.InvokeAsync(args: (DiscordMessage)gatewayEvent.EventData)
								.ConfigureAwait(continueOnCapturedContext: false);
							break;

						// case DiscordEventType.MessageCreate:
						// 	await Task
						// 		.WhenAll(
						// 			tasks: _handlersByEventType[key: DiscordEventType.MessageCreate]
						// 				.Cast<IDiscordAsyncEventService.HandleMessageAsync>()
						// 				.Select(selector: v => v(message: (DiscordMessage)gatewayEvent.EventData))
						// 		)
						// 		.ConfigureAwait(continueOnCapturedContext: false);
						// 	break;
						//
						// case DiscordEventType.MessageDelete:
						// 	await Task
						// 		.WhenAll(
						// 			tasks: _handlersByEventType[key: DiscordEventType.MessageDelete]
						// 				.Cast<IDiscordAsyncEventService.HandleMessageDeleteAsync>()
						// 				.Select(selector: v => v(message: (DiscordMessageDelete)gatewayEvent.EventData))
						// 		)
						// 		.ConfigureAwait(continueOnCapturedContext: false);
						// 	break;
						//
						// case DiscordEventType.MessageUpdate:
						// 	await Task
						// 		.WhenAll(
						// 			tasks: _handlersByEventType[key: DiscordEventType.MessageUpdate]
						// 				.Cast<IDiscordAsyncEventService.HandleMessageUpdateAsync>()
						// 				.Select(selector: v => v(message: (DiscordUpdatedMessage)gatewayEvent.EventData))
						// 		)
						// 		.ConfigureAwait(continueOnCapturedContext: false);
						// 	break;
						//
						// case DiscordEventType.MessageReactionAdd:
						// 	await Task
						// 		.WhenAll(
						// 			tasks: _handlersByEventType[key: DiscordEventType.MessageReactionAdd]
						// 				.Cast<IDiscordAsyncEventService.HandleMessageReactionAddAsync>()
						// 				.Select(
						// 					selector: v => v(messageReaction: (DiscordMessageReactionAdd)gatewayEvent.EventData)
						// 				)
						// 		)
						// 		.ConfigureAwait(continueOnCapturedContext: false);
						// 	break;
					}
				}
				catch (OperationCanceledException)
				{
					return;
				}
				catch (Exception exception)
				{
					Log.Error(exception: exception, messageTemplate: "Unhandled exception from gateway event handler.");
				}
			}
		}
		catch (OperationCanceledException)
		{
		}
	}
}