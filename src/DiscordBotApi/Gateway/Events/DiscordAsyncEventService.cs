// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordAsyncEventService.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Concurrent;

using DiscordBotApi.Models.Gateway.Events;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Interactions;

using Serilog;

namespace DiscordBotApi.Gateway.Events;

public class DiscordAsyncEventService
{
	public delegate Task HandleGuildMemberAddAsync(DiscordGuildMemberAdd member);

	public delegate Task HandleInteractionAsync(DiscordInteraction interaction);

	public delegate Task HandleMessageAsync(DiscordMessage message);

	public delegate Task HandleMessageReactionAddAsync(DiscordMessageReactionAdd messageReaction);

	private readonly DiscordBotClient _botClient;
	private readonly CancellationTokenSource _cancellationTokenSource = new();
	private readonly BlockingCollection<DiscordGatewayEvent> _gatewayEvents = new();
	private readonly HashSet<DiscordEventType> _handledEventTypes = new();
	private readonly object _handledEventTypesLock = new();
	private readonly Dictionary<DiscordEventType, List<object>> _handlersByEventType = new();
	private readonly Task _processingTask;

	public DiscordAsyncEventService(DiscordBotClient botClient)
	{
		_botClient = botClient;

		// TODO: Maybe add a method to stop/dispose if needed.
		_processingTask = Task.Factory.StartNew(
				function: async () => await ProcessGatewayEventsAsync(cancellationToken: _cancellationTokenSource.Token)
					.ConfigureAwait(continueOnCapturedContext: false),
				cancellationToken: _cancellationTokenSource.Token,
				creationOptions: TaskCreationOptions.LongRunning,
				scheduler: TaskScheduler.Default)
			.Unwrap();
	}

	// TODO: Maybe add corresponding Unhandle... methods if that is needed.
	public void HandleGuildMemberAdd(HandleGuildMemberAddAsync handler) =>
		AddHandler(
			eventType: DiscordEventType.GuildMemberAdd,
			subscribeAction: () => _botClient.GuildMemberAdd += OnGuildMemberAdd,
			handleDelegate: handler);

	public void HandleInteractionCreate(HandleInteractionAsync handler) =>
		AddHandler(
			eventType: DiscordEventType.InteractionCreate,
			subscribeAction: () => _botClient.InteractionCreate += OnInteractionCreate,
			handleDelegate: handler);

	public void HandleMessageCreate(HandleMessageAsync handler) =>
		AddHandler(
			eventType: DiscordEventType.MessageCreate,
			subscribeAction: () => _botClient.MessageCreate += OnMessageCreate,
			handleDelegate: handler);

	public void HandleMessageReactionAdd(HandleMessageAsync handler) =>
		AddHandler(
			eventType: DiscordEventType.MessageReactionAdd,
			subscribeAction: () => _botClient.MessageReactionAdd += OnMessageReactionAdd,
			handleDelegate: handler);

	private void AddHandler(DiscordEventType eventType, Action subscribeAction, object handleDelegate)
	{
		lock (_handledEventTypesLock)
		{
			if (!_handledEventTypes.Contains(item: eventType))
			{
				subscribeAction();
				_handledEventTypes.Add(item: eventType);
			}

			if (!_handlersByEventType.ContainsKey(key: eventType))
			{
				_handlersByEventType.Add(key: eventType, value: new List<object>());
			}

			_handlersByEventType[key: eventType]
				.Add(item: handleDelegate);
		}
	}

	private void OnGuildMemberAdd(object? _, DiscordGuildMemberAdd member) =>
		_gatewayEvents.Add(item: new DiscordGatewayEvent(EventType: DiscordEventType.GuildMemberAdd, EventData: member));

	private void OnInteractionCreate(object? _, DiscordInteraction interaction) =>
		_gatewayEvents.Add(item: new DiscordGatewayEvent(EventType: DiscordEventType.InteractionCreate, EventData: interaction));

	private void OnMessageCreate(object? _, DiscordMessage message) =>
		_gatewayEvents.Add(item: new DiscordGatewayEvent(EventType: DiscordEventType.MessageCreate, EventData: message));

	private void OnMessageReactionAdd(object? _, DiscordMessageReactionAdd messageReaction) =>
		_gatewayEvents.Add(
			item: new DiscordGatewayEvent(EventType: DiscordEventType.MessageReactionAdd, EventData: messageReaction));

	private async Task ProcessGatewayEventsAsync(CancellationToken cancellationToken)
	{
		try
		{
			foreach (var gatewayEvent in _gatewayEvents.GetConsumingEnumerable())
			{
				if (cancellationToken.IsCancellationRequested)
				{
					break;
				}

				try
				{
					switch (gatewayEvent.EventType)
					{
						case DiscordEventType.GuildMemberAdd:
							await Task.WhenAll(
									tasks: _handlersByEventType[key: DiscordEventType.GuildMemberAdd]
										.Cast<HandleGuildMemberAddAsync>()
										.Select(selector: v => v(member: (DiscordGuildMemberAdd)gatewayEvent.EventData)))
								.ConfigureAwait(continueOnCapturedContext: false);
							break;

						case DiscordEventType.InteractionCreate:
							await Task.WhenAll(
									tasks: _handlersByEventType[key: DiscordEventType.InteractionCreate]
										.Cast<HandleInteractionAsync>()
										.Select(selector: v => v(interaction: (DiscordInteraction)gatewayEvent.EventData)))
								.ConfigureAwait(continueOnCapturedContext: false);
							break;

						case DiscordEventType.MessageCreate:
							await Task.WhenAll(
									tasks: _handlersByEventType[key: DiscordEventType.MessageCreate]
										.Cast<HandleMessageAsync>()
										.Select(selector: v => v(message: (DiscordMessage)gatewayEvent.EventData)))
								.ConfigureAwait(continueOnCapturedContext: false);
							break;

						case DiscordEventType.MessageReactionAdd:
							await Task.WhenAll(
									tasks: _handlersByEventType[key: DiscordEventType.MessageReactionAdd]
										.Cast<HandleMessageReactionAddAsync>()
										.Select(selector: v => v(messageReaction: (DiscordMessageReactionAdd)gatewayEvent.EventData)))
								.ConfigureAwait(continueOnCapturedContext: false);
							break;
					}
				}
				catch (Exception exception)
				{
					if (exception is OperationCanceledException operationCanceledException)
					{
						throw;
					}

					Log.Error(exception: exception, messageTemplate: "Unhandled exception from gateway event handler.");
				}
			}
		}
		catch (OperationCanceledException)
		{
		}
	}

	private record DiscordGatewayEvent(DiscordEventType EventType, object EventData);
}