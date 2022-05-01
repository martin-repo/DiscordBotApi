// -------------------------------------------------------------------------------------------------
// <copyright file="TaskCompletionSourceCreator.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.EndToEndTests
{
    using System;
    using System.Threading.Tasks;

    public static class TaskCompletionSourceCreator
    {
        public static TaskCompletionSource<TResult> Create<TResult>(TimeSpan? lifetime = null)
        {
            lifetime ??= TimeSpan.FromSeconds(5);

            var completion = new TaskCompletionSource<TResult>(TaskCreationOptions.RunContinuationsAsynchronously);

            _ = Task.Run(
                async () =>
                {
                    await Task.Delay(lifetime.Value);
                    completion.TrySetCanceled();
                });

            return completion;
        }
    }
}