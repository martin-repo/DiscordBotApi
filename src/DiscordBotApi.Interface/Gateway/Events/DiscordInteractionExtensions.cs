// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionExtensions.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;
using DiscordBotApi.Interface.Models.Interactions;
using DiscordBotApi.Interface.Models.Webhooks;

namespace DiscordBotApi.Interface.Gateway.Events;

public static class DiscordInteractionExtensions
{
	/// <summary>
	/// Create an ephemeral followup message. This will replace any loading message.
	/// </summary>
	/// <remarks>
	/// This can be called multiple times per interaction. But only 5 times if called by a user when outside a guild.
	/// </remarks>
	public static async Task<DiscordMessage> CreateEphemeralFollowupMessageAsync(
		this DiscordInteraction interaction,
		IDiscordBotClient botClient,
		string? content = null,
		IReadOnlyCollection<DiscordMessageComponent>? components = null
	) =>
		await botClient
			.CreateFollowupMessageAsync(
				applicationId: interaction.ApplicationId,
				interactionToken: interaction.Token,
				args: new DiscordExecuteWebhookArgs
				{
					Content = content,
					Components = components,
					Flags = DiscordMessageFlags.Ephemeral
				}
			)
			.ConfigureAwait(continueOnCapturedContext: false);

	/// <summary>
	/// Create an ephemeral response message with a loading indicator.
	/// </summary>
	/// <remarks>
	/// This message will be replaced when a followup message is sent.
	/// </remarks>
	public static async Task CreateEphemeralLoadingMessageAsync(
		this DiscordInteraction interaction,
		IDiscordBotClient botClient,
		FollowupAction followupAction
	) =>
		await botClient
			.CreateInteractionResponseAsync(
				interactionId: interaction.Id,
				interactionToken: interaction.Token,
				args: new DiscordInteractionResponseArgs
				{
					Type = followupAction switch
					{
						FollowupAction.CreateMessage => DiscordInteractionCallbackType.DeferredChannelMessageWithSource,
						FollowupAction.EditMessage => DiscordInteractionCallbackType.DeferredUpdateMessage,
						_ => throw new InvalidOperationException(message: "Invalid followup action.")
					},
					Data = new DiscordInteractionCallbackMessage { Flags = DiscordMessageFlags.Ephemeral }
				}
			)
			.ConfigureAwait(continueOnCapturedContext: false);

	public static async Task DeleteFirstFollowupMessageAsync(
		this DiscordInteraction interaction,
		IDiscordBotClient botClient
	) =>
		await botClient
			.DeleteOriginalInteractionResponseAsync(
				applicationId: interaction.ApplicationId,
				interactionToken: interaction.Token
			)
			.ConfigureAwait(continueOnCapturedContext: false);

	public static async Task EditFirstFollowupMessageAsync(
		this DiscordInteraction interaction,
		IDiscordBotClient botClient,
		string? content = null,
		IReadOnlyCollection<DiscordEmbed>? embeds = null,
		IReadOnlyCollection<DiscordMessageComponent>? components = null
	) =>
		await botClient
			.EditOriginalInteractionResponseAsync(
				applicationId: interaction.ApplicationId,
				interactionToken: interaction.Token,
				args: new DiscordEditWebhookMessageArgs
				{
					Content = content,
					Embeds = embeds,
					Components = components
				}
			)
			.ConfigureAwait(continueOnCapturedContext: false);
}