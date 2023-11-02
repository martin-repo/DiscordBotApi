// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEventHandlerBase.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

using DiscordBotApi.Models.Applications;
using DiscordBotApi.Models.Guilds;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Interactions;

namespace DiscordBotApi.Gateway.Events;

public abstract class DiscordEventHandlerBase : IDiscordEventHandler
{
	public abstract IEnumerable<DiscordEventType> EventTypes { get; }

	/// <returns>
	/// True if handled, and remaining handlers should be skipped. False if remaining handlers should be called.
	/// </returns>
	public virtual Task<bool> HandleGuildMemberAddAsync(DiscordGuildMember member) => Task.FromResult(result: false);

	/// <returns>
	/// True if handled, and remaining handlers should be skipped. False if remaining handlers should be called.
	/// </returns>
	public virtual Task<bool> HandleInteractionCreateAsync(DiscordInteraction interaction) => Task.FromResult(result: false);

	/// <returns>
	/// True if handled, and remaining handlers should be skipped. False if remaining handlers should be called.
	/// </returns>
	public virtual Task<bool> HandleMessageCreateAsync(DiscordMessage message) => Task.FromResult(result: false);

	public virtual Task InitializeAsync(
		ImmutableArray<DiscordApplicationCommand> currentGlobalCommands,
		ImmutableArray<DiscordApplicationCommand>? currentGuildCommands
	) =>
		Task.CompletedTask;

	protected static async Task
		CreateInteractionResponseAsync(DiscordBotClient botClient, DiscordInteraction interaction, string message) =>
		await botClient.CreateInteractionResponseAsync(
				interactionId: interaction.Id,
				interactionToken: interaction.Token,
				args: new DiscordInteractionResponseArgs
				{
					Type = DiscordInteractionCallbackType.ChannelMessageWithSource,
					Data = new DiscordInteractionCallbackMessage
					{
						Content = message,
						Flags = DiscordMessageFlags.Ephemeral
					}
				})
			.ConfigureAwait(continueOnCapturedContext: false);

	protected static async Task DeferMessageCreationAsync(DiscordBotClient botClient, DiscordInteraction interaction) =>
		await botClient.CreateInteractionResponseAsync(
				interactionId: interaction.Id,
				interactionToken: interaction.Token,
				args: new DiscordInteractionResponseArgs
				{
					Type = DiscordInteractionCallbackType.DeferredChannelMessageWithSource,
					Data = new DiscordInteractionCallbackMessage { Flags = DiscordMessageFlags.Ephemeral }
				})
			.ConfigureAwait(continueOnCapturedContext: false);

	protected static bool TryGetOption(
		IEnumerable<DiscordApplicationCommandInteractionDataOption>? options,
		string name,
		[NotNullWhen(returnValue: true)] out DiscordApplicationCommandInteractionDataOption? value
	)
	{
		value = options?.FirstOrDefault(predicate: v => v.Name == name);
		return value is not null;
	}

	protected static bool TryGetOptionBoolean(
		IEnumerable<DiscordApplicationCommandInteractionDataOption>? options,
		string name,
		[NotNullWhen(returnValue: true)] out bool? value
	)
	{
		var option = options?.FirstOrDefault(predicate: v => v.Name == name);
		if (option is null)
		{
			value = null;
			return false;
		}

		value = option.Value is bool boolValue
			? boolValue
			: null;
		return value is not null;
	}

	protected static bool TryGetOptionString(
		IEnumerable<DiscordApplicationCommandInteractionDataOption>? options,
		string name,
		[NotNullWhen(returnValue: true)] out string? value
	)
	{
		var option = options?.FirstOrDefault(predicate: v => v.Name == name);
		if (option is null)
		{
			value = null;
			return false;
		}

		value = (option.Value as string)?.Trim();
		return !string.IsNullOrWhiteSpace(value: value);
	}
}