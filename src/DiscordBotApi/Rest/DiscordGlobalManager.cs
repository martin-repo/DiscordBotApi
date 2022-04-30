// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGlobalManager.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Rest
{
    using System.Collections.Concurrent;

    using DiscordBotApi.Models.Rest;

    using Serilog;

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
            if (resourceId.Path.StartsWith("interactions", StringComparison.Ordinal))
            {
                return;
            }

            if (_globalQueue == null)
            {
                throw new InvalidOperationException("Not started");
            }

            if (_globalQueue.Count > MaxQueuedReservations)
            {
                throw new InvalidOperationException("Too many requests");
            }

            var ready = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            _globalQueue.Add(ready, cancellationToken);

            await ready.Task.WaitAsync(cancellationToken).ConfigureAwait(false);
        }

        public void Start()
        {
            if (_globalQueue != null)
            {
                throw new InvalidOperationException("Already started");
            }

            var queue = _globalQueue = new BlockingCollection<TaskCompletionSource>(new ConcurrentQueue<TaskCompletionSource>());
            new TaskFactory().StartNew(async () => await GlobalProcessingAsync(queue).ConfigureAwait(false), TaskCreationOptions.LongRunning);
        }

        public void Stop()
        {
            if (_globalQueue == null)
            {
                throw new InvalidOperationException("Already stopped");
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
                if (!queue.TryTake(out var item, Timeout.Infinite))
                {
                    continue;
                }

                var utcNow = DateTime.UtcNow;
                if (globalEnding < utcNow)
                {
                    globalEnding = utcNow.AddSeconds(1);
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
                _logger?.Debug("Discord preemptive rate limit; waiting {Count:F2} seconds", retryAfter.TotalSeconds);
                await Task.Delay(retryAfter).ConfigureAwait(false);

                globalEnding = utcNow.AddSeconds(1);
                globalCount = _globalLimit - 1;
                item.SetResult();
            }
        }
    }
}