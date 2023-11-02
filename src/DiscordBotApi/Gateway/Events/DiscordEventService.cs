// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEventService.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;

using DiscordBotApi.Models.Applications;

using Serilog;

namespace DiscordBotApi.Gateway.Events;

public class DiscordEventService
{
	private readonly DiscordBotClient _botClient;
	private readonly ImmutableArray<IDiscordEventHandler> _handlers;
	private readonly ImmutableDictionary<DiscordEventType, ImmutableArray<IDiscordEventHandler>> _handlersByEventType;

	public DiscordEventService(DiscordBotClient botClient, IEnumerable<IDiscordEventHandler> handlers)
	{
		_botClient = botClient;
		_handlers = handlers.ToImmutableArray();
		_handlersByEventType = _handlers.SelectMany(
				selector: v => v.EventTypes.Select(
					selector: t => new
					{
						EventType = t,
						Handler = v
					}))
			.GroupBy(keySelector: v => v.EventType)
			.ToImmutableDictionary(
				keySelector: v => v.Key,
				elementSelector: v => v.Select(selector: x => x.Handler)
					.ToImmutableArray());

		foreach (var eventType in _handlersByEventType.Keys)
		{
			switch (eventType)
			{
				case DiscordEventType.GuildMemberAdd:
					_botClient.GuildMemberAdd += async (_, member) => await HandleEventAsync(
							eventType: eventType,
							eventHandlerFunc: v => v.HandleGuildMemberAddAsync(member: member))
						.ConfigureAwait(continueOnCapturedContext: false);
					break;
				case DiscordEventType.InteractionCreate:
					_botClient.InteractionCreate += async (_, interaction) => await HandleEventAsync(
							eventType: eventType,
							eventHandlerFunc: v => v.HandleInteractionCreateAsync(interaction: interaction))
						.ConfigureAwait(continueOnCapturedContext: false);
					break;
				case DiscordEventType.MessageCreate:
					_botClient.MessageCreate += async (_, message) => await HandleEventAsync(
							eventType: eventType,
							eventHandlerFunc: v => v.HandleMessageCreateAsync(message: message))
						.ConfigureAwait(continueOnCapturedContext: false);
					break;
				default:
					throw new ArgumentOutOfRangeException(paramName: $"{nameof(DiscordEventType)} {eventType} is not supported.");
			}
		}
	}

	public async Task InitializeAsync(ulong applicationId, ulong? guildId)
	{
		var currentGlobalCommands = await _botClient.GetGlobalApplicationCommandsAsync(applicationId: applicationId)
			.ConfigureAwait(continueOnCapturedContext: false);
		var currentGuildCommands = guildId is { } guildIdValue
			? await _botClient.GetGuildApplicationCommandsAsync(applicationId: applicationId, guildId: guildIdValue)
				.ConfigureAwait(continueOnCapturedContext: false)
			: (ImmutableArray<DiscordApplicationCommand>?)null;

		foreach (var handler in _handlers)
		{
			await handler.InitializeAsync(currentGlobalCommands: currentGlobalCommands, currentGuildCommands: currentGuildCommands)
				.ConfigureAwait(continueOnCapturedContext: false);
		}
	}

	// https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/async-scenarios#important-info-and-advice
	// - Exceptions thrown in an async void method can't be caught outside of that method.
	// - async void methods are difficult to test.
	// - async void methods can cause bad side effects if the caller isn't expecting them to be async.
	private async Task HandleEventAsync(DiscordEventType eventType, Func<IDiscordEventHandler, Task<bool>> eventHandlerFunc)
	{
		try
		{
			foreach (var handler in _handlersByEventType[key: eventType])
			{
				if (await eventHandlerFunc(arg: handler)
					.ConfigureAwait(continueOnCapturedContext: false))
				{
					return;
				}
			}
		}
		catch (Exception exception)
		{
			// Do not allow exceptions from other threads to propagate, they will *destroy* the Discord API...
			Log.Error(exception: exception, messageTemplate: $"Unhandled exception in {nameof(DiscordEventService)}.");
		}
	}
}