// -------------------------------------------------------------------------------------------------
// <copyright file="EventHandlerManager.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Concurrent;
using System.Collections.Immutable;

using DiscordBotApi.Models.Gateway.Events;

namespace DiscordBotApi.Gateway.Events;

internal class EventHandlerManager<T>(
	BlockingCollection<(DiscordEventType EventType, object EventData)> gatewayEvents,
	DiscordEventType eventType,
	Action<EventHandler<T>> subscribe,
	Action<EventHandler<T>> unsubscribe
)
	where T : class
{
	private readonly Lock _handlerLock = new();
	private readonly List<Func<T, Task>> _handlers = [];

	public void Add(Func<T, Task> handler)
	{
		lock (_handlerLock)
		{
			if (_handlers.Count == 0)
			{
				subscribe(obj: OnBotEvent);
			}

			_handlers.Add(item: handler);
		}
	}

	public async Task InvokeAsync(T args)
	{
		ImmutableArray<Func<T, Task>> handlers;
		lock (_handlerLock)
		{
			handlers = _handlers.ToImmutableArray();
		}

		foreach (var handler in handlers)
		{
			await handler(arg: args).ConfigureAwait(continueOnCapturedContext: false);
		}
	}

	public void Remove(Func<T, Task> handler)
	{
		lock (_handlerLock)
		{
			_handlers.Remove(item: handler);
			if (_handlers.Count == 0)
			{
				unsubscribe(obj: OnBotEvent);
			}
		}
	}

	private void OnBotEvent(object? _, T eventData) =>
		gatewayEvents.Add(item: (EventType: eventType, EventData: eventData));
}