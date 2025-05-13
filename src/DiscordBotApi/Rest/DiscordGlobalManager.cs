// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGlobalManager.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Concurrent;

using DiscordBotApi.Models.Rest;

using Serilog;

namespace DiscordBotApi.Rest;

internal class DiscordGlobalManager : IDiscordGlobalManager
{
	private const int MaxQueuedReservations = 200;

	private readonly int _globalLimit;
	private readonly ILogger? _logger;

	private BlockingCollection<TaskCompletionSource>? _globalQueue;

	public DiscordGlobalManager(int globalLimit, ILogger? logger)
	{
		_globalLimit = globalLimit;
		_logger = logger?.ForContext<DiscordGlobalManager>();
	}

	public async Task GetReservationAsync(DiscordResourceId resourceId, CancellationToken cancellationToken)
	{
		if (resourceId.Path.StartsWith(value: "interactions", comparisonType: StringComparison.Ordinal))
		{
			return;
		}

		if (_globalQueue == null)
		{
			throw new InvalidOperationException(message: "Not started");
		}

		if (_globalQueue.Count > MaxQueuedReservations)
		{
			throw new InvalidOperationException(message: "Too many requests");
		}

		var ready = new TaskCompletionSource(creationOptions: TaskCreationOptions.RunContinuationsAsynchronously);
		_globalQueue.Add(item: ready, cancellationToken: cancellationToken);

		await ready.Task.WaitAsync(cancellationToken: cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
	}

	public void Start()
	{
		if (_globalQueue != null)
		{
			throw new InvalidOperationException(message: "Already started");
		}

		_globalQueue =
			new BlockingCollection<TaskCompletionSource>(collection: new ConcurrentQueue<TaskCompletionSource>());
		var queue = _globalQueue;

		new TaskFactory().StartNew(
			function: async () => await GlobalProcessingAsync(queue: queue)
				.ConfigureAwait(continueOnCapturedContext: false),
			creationOptions: TaskCreationOptions.LongRunning
		);
	}

	public void Stop()
	{
		if (_globalQueue == null)
		{
			throw new InvalidOperationException(message: "Already stopped");
		}

		_globalQueue.CompleteAdding();
		_globalQueue = null;
	}

	private async Task GlobalProcessingAsync(BlockingCollection<TaskCompletionSource> queue)
	{
		var globalCount = 0;
		var globalEnding = DateTime.MinValue;

		while (!queue.IsCompleted)
		{
			if (!queue.TryTake(item: out var item, millisecondsTimeout: Timeout.Infinite))
			{
				continue;
			}

			var utcNow = DateTime.UtcNow;
			if (globalEnding < utcNow)
			{
				globalEnding = utcNow.AddSeconds(value: 1);
				globalCount = _globalLimit - 1;
				item.SetResult();
				continue;
			}

			if (globalCount > 0)
			{
				globalCount--;
				item.SetResult();
				continue;
			}

			var retryAfter = globalEnding - utcNow;
			_logger?.Debug(
				messageTemplate: "Discord preemptive rate limit; waiting {Count:F2} seconds",
				propertyValue: retryAfter.TotalSeconds
			);
			await Task.Delay(delay: retryAfter).ConfigureAwait(continueOnCapturedContext: false);

			globalEnding = utcNow.AddSeconds(value: 1);
			globalCount = _globalLimit - 1;
			item.SetResult();
		}
	}
}