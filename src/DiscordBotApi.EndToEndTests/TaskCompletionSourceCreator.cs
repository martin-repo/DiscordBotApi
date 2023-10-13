// -------------------------------------------------------------------------------------------------
// <copyright file="TaskCompletionSourceCreator.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace DiscordBotApi.EndToEndTests;

public static class TaskCompletionSourceCreator
{
	public static TaskCompletionSource<TResult> Create<TResult>(TimeSpan? lifetime = null)
	{
		lifetime ??= TimeSpan.FromSeconds(value: 5);

		var completion = new TaskCompletionSource<TResult>(creationOptions: TaskCreationOptions.RunContinuationsAsynchronously);

		_ = Task.Run(
			function: async () =>
			{
				await Task.Delay(delay: lifetime.Value);
				completion.TrySetCanceled();
			});

		return completion;
	}
}