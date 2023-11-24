// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionUtils.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

using DiscordBotApi.Models.Applications;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Models.Interactions;
using DiscordBotApi.Models.Webhooks;

namespace DiscordBotApi.Gateway.Events;

public static class DiscordInteractionUtils
{
	/// <summary>
	/// Completes (applies) an interaction, initiated from DeferMessageCreationAsync.
	/// </summary>
	public static async Task CompleteMessageCreationAsync(
		DiscordBotClient botClient,
		DiscordInteraction interaction,
		ulong applicationId,
		string? content = null,
		IReadOnlyCollection<DiscordMessageComponent>? components = null,
		bool isEphemeral = true
	) =>
		await botClient.CreateFollowupMessageAsync(
				applicationId: applicationId,
				interactionToken: interaction.Token,
				args: new DiscordExecuteWebhookArgs
				{
					Content = content,
					Components = components,
					Flags = isEphemeral
						? DiscordMessageFlags.Ephemeral
						: null
				})
			.ConfigureAwait(continueOnCapturedContext: false);

	/// <summary>
	/// Completes (applies) an interaction, initiated from DeferMessageUpdateAsync.
	/// </summary>
	public static async Task CompleteMessageUpdateAsync(
		DiscordBotClient botClient,
		DiscordInteraction interaction,
		ulong applicationId,
		string? content = null,
		IReadOnlyCollection<DiscordMessageComponent>? components = null
	) =>
		await botClient.EditOriginalInteractionResponseAsync(
				applicationId: applicationId,
				interactionToken: interaction.Token,
				original: interaction.Message!.Id,
				args: new DiscordEditWebhookMessageArgs
				{
					Content = content,
					Components = components
				})
			.ConfigureAwait(continueOnCapturedContext: false);

	public static async Task CreateMessageAsync(
		DiscordBotClient botClient,
		DiscordInteraction interaction,
		string message,
		IReadOnlyCollection<DiscordMessageComponent>? components = null,
		bool isEphemeral = true
	) =>
		await botClient.CreateInteractionResponseAsync(
				interactionId: interaction.Id,
				interactionToken: interaction.Token,
				args: new DiscordInteractionResponseArgs
				{
					Type = DiscordInteractionCallbackType.ChannelMessageWithSource,
					Data = new DiscordInteractionCallbackMessage
					{
						Content = message,
						Components = components,
						Flags = isEphemeral
							? DiscordMessageFlags.Ephemeral
							: null
					}
				})
			.ConfigureAwait(continueOnCapturedContext: false);

	/// <summary>
	/// Defers creation of an ephemeral message. Call CompleteMessageCreationAsync when contents are ready.
	/// </summary>
	public static async Task DeferMessageCreationAsync(DiscordBotClient botClient, DiscordInteraction interaction) =>
		await botClient.CreateInteractionResponseAsync(
				interactionId: interaction.Id,
				interactionToken: interaction.Token,
				args: new DiscordInteractionResponseArgs
				{
					Type = DiscordInteractionCallbackType.DeferredChannelMessageWithSource,
					Data = new DiscordInteractionCallbackMessage { Flags = DiscordMessageFlags.Ephemeral }
				})
			.ConfigureAwait(continueOnCapturedContext: false);

	/// <summary>
	/// Defers update of an ephemeral message. Call CompleteMessageUpdateAsync when contents are ready.
	/// </summary>
	public static async Task DeferMessageUpdateAsync(DiscordBotClient botClient, DiscordInteraction interaction) =>
		await botClient.CreateInteractionResponseAsync(
				interactionId: interaction.Id,
				interactionToken: interaction.Token,
				args: new DiscordInteractionResponseArgs
				{
					Type = DiscordInteractionCallbackType.DeferredUpdateMessage,
					Data = new DiscordInteractionCallbackMessage { Flags = DiscordMessageFlags.Ephemeral }
				})
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

		value = option.Value is bool boolValue
			? boolValue
			: null;
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

	public static async Task UpdateMessageAsync(
		DiscordBotClient botClient,
		DiscordInteraction interaction,
		string message,
		IReadOnlyCollection<DiscordMessageComponent>? components = null
	) =>
		await botClient.CreateInteractionResponseAsync(
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
				})
			.ConfigureAwait(continueOnCapturedContext: false);
}