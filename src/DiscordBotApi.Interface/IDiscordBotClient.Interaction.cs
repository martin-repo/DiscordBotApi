// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordBotClient.Interaction.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Interface.Models.Interactions;
using DiscordBotApi.Interface.Models.Webhooks;

namespace DiscordBotApi.Interface;

public partial interface IDiscordBotClient
{
	Task<DiscordMessage> CreateFollowupMessageAsync(
		ulong applicationId,
		string interactionToken,
		DiscordExecuteWebhookArgs args,
		CancellationToken cancellationToken = default
	);

	Task CreateInteractionResponseAsync(
		ulong interactionId,
		string interactionToken,
		DiscordInteractionResponseArgs args,
		CancellationToken cancellationToken = default
	);

	Task DeleteOriginalInteractionResponseAsync(
		ulong applicationId,
		string interactionToken,
		CancellationToken cancellationToken = default
	);

	Task<DiscordMessage> EditOriginalInteractionResponseAsync(
		ulong applicationId,
		string interactionToken,
		DiscordEditWebhookMessageArgs args,
		CancellationToken cancellationToken = default
	);

	Task<DiscordMessage> GetOriginalInteractionResponseAsync(
		ulong applicationId,
		string interactionToken,
		CancellationToken cancellationToken = default
	);
}