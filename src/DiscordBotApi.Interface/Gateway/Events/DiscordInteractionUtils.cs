// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionUtils.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

using DiscordBotApi.Interface.Models.Applications;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Interface.Models.Interactions;
using DiscordBotApi.Interface.Models.Webhooks;

namespace DiscordBotApi.Interface.Gateway.Events;

public static class DiscordInteractionUtils
{
	/// <summary>
	/// Completes (applies) an interaction, initiated from DeferMessageCreationAsync.
	/// </summary>
	/// <remarks>
	/// It is possible to call this methods multiple times for a single interaction.
	/// Limitation: For interactions originating from a user (i.e. not from within a guild), this can be done up to 5 times.
	/// </remarks>
	public static async Task<DiscordMessage> CompleteMessageCreationAsync(
		IDiscordBotClient botClient,
		DiscordInteraction interaction,
		ulong applicationId,
		string? content = null,
		IReadOnlyCollection<DiscordMessageComponent>? components = null,
		bool isEphemeral = true
	) =>
		await botClient
			.CreateFollowupMessageAsync(
				applicationId: applicationId,
				interactionToken: interaction.Token,
				args: new DiscordExecuteWebhookArgs
				{
					Content = content,
					Components = components,
					Flags = isEphemeral ? DiscordMessageFlags.Ephemeral : null
				}
			)
			.ConfigureAwait(continueOnCapturedContext: false);

	/// <summary>
	/// Completes (applies) an interaction, initiated from DeferMessageUpdateAsync.
	/// </summary>
	public static async Task CompleteMessageUpdateAsync(
		IDiscordBotClient botClient,
		DiscordInteraction interaction,
		ulong applicationId,
		string? content = null,
		IReadOnlyCollection<DiscordMessageComponent>? components = null
	) =>
		await botClient
			.EditOriginalInteractionResponseAsync(
				applicationId: applicationId,
				interactionToken: interaction.Token,
				args: new DiscordEditWebhookMessageArgs
				{
					Content = content,
					Components = components
				}
			)
			.ConfigureAwait(continueOnCapturedContext: false);

	/// <summary>
	/// Respond to an interaction with a message.
	/// </summary>
	/// <remarks>
	/// This must be done within 3 seconds. Use DeferMessageCreationAsync/CompleteMessageCreationAsync instead if more time is needed.
	/// </remarks>
	public static async Task CreateMessageAsync(
		IDiscordBotClient botClient,
		DiscordInteraction interaction,
		string message,
		IReadOnlyCollection<DiscordMessageComponent>? components = null,
		bool isEphemeral = true
	) =>
		await botClient
			.CreateInteractionResponseAsync(
				interactionId: interaction.Id,
				interactionToken: interaction.Token,
				args: new DiscordInteractionResponseArgs
				{
					Type = DiscordInteractionCallbackType.ChannelMessageWithSource,
					Data = new DiscordInteractionCallbackMessage
					{
						Content = message,
						Components = components,
						Flags = isEphemeral ? DiscordMessageFlags.Ephemeral : null
					}
				}
			)
			.ConfigureAwait(continueOnCapturedContext: false);

	/// <summary>
	/// Defers creation of an ephemeral message. Call CompleteMessageCreationAsync when contents are ready.
	/// </summary>
	public static async Task DeferMessageCreationAsync(IDiscordBotClient botClient, DiscordInteraction interaction) =>
		await botClient
			.CreateInteractionResponseAsync(
				interactionId: interaction.Id,
				interactionToken: interaction.Token,
				args: new DiscordInteractionResponseArgs
				{
					Type = DiscordInteractionCallbackType.DeferredChannelMessageWithSource,
					Data = new DiscordInteractionCallbackMessage { Flags = DiscordMessageFlags.Ephemeral }
				}
			)
			.ConfigureAwait(continueOnCapturedContext: false);

	/// <summary>
	/// Defers update of an ephemeral message. Call CompleteMessageUpdateAsync when contents are ready.
	/// </summary>
	public static async Task DeferMessageUpdateAsync(IDiscordBotClient botClient, DiscordInteraction interaction) =>
		await botClient
			.CreateInteractionResponseAsync(
				interactionId: interaction.Id,
				interactionToken: interaction.Token,
				args: new DiscordInteractionResponseArgs
				{
					Type = DiscordInteractionCallbackType.DeferredUpdateMessage,
					Data = new DiscordInteractionCallbackMessage { Flags = DiscordMessageFlags.Ephemeral }
				}
			)
			.ConfigureAwait(continueOnCapturedContext: false);

	public static bool TryGetOption(
		IEnumerable<DiscordApplicationCommandInteractionDataOption>? options,
		string name,
		[NotNullWhen(returnValue: true)] out DiscordApplicationCommandInteractionDataOption? value
	)
	{
		value = options?.FirstOrDefault(predicate: v => v.Name == name);
		return value is not null;
	}

	public static bool TryGetOptionBoolean(
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

		value = option.Value is bool boolValue ? boolValue : null;
		return value is not null;
	}

	public static bool TryGetOptionString(
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

	/// <summary>
	/// Respond to an interaction by updating the source message.
	/// </summary>
	/// <remarks>
	/// This must be done within 3 seconds. Use DeferMessageUpdateAsync/CompleteMessageUpdateAsync instead if more time is needed.
	/// </remarks>
	public static async Task UpdateMessageAsync(
		IDiscordBotClient botClient,
		DiscordInteraction interaction,
		string message,
		IReadOnlyCollection<DiscordMessageComponent>? components = null
	) =>
		await botClient
			.CreateInteractionResponseAsync(
				interactionId: interaction.Id,
				interactionToken: interaction.Token,
				args: new DiscordInteractionResponseArgs
				{
					Type = DiscordInteractionCallbackType.UpdateMessage,
					Data = new DiscordInteractionCallbackMessage
					{
						Content = message,
						Components = components
					}
				}
			)
			.ConfigureAwait(continueOnCapturedContext: false);
}