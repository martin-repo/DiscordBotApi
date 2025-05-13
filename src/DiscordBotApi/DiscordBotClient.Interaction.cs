// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClient.Interaction.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Net;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Interface.Models.Interactions;
using DiscordBotApi.Interface.Models.Webhooks;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Interactions;
using DiscordBotApi.Models.Webhooks;

namespace DiscordBotApi;

public partial class DiscordBotClient
{
	// https://discord.com/developers/docs/interactions/receiving-and-responding#create-followup-message
	public async Task<DiscordMessage> CreateFollowupMessageAsync(
		ulong applicationId,
		string interactionToken,
		DiscordExecuteWebhookArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"webhooks/{applicationId}/{interactionToken}";

		var argsDto = DiscordExecuteWebhookArgsDto.FromModel(model: args);
		var payloadJson = _restClient.CreateJsonContent(value: argsDto);

		using var content = CreateContentForMessage(
			attachments: args.Attachments,
			files: args.Files,
			payloadJson: payloadJson
		);

		// ReSharper disable once AccessToDisposedClosure
		var messageDto = await _restClient
			.SendRequestAsync<DiscordMessageDto>(
				requestFactoryFunc: () =>
					new HttpRequestMessage(method: HttpMethod.Post, requestUri: url) { Content = content },
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);

		var message = messageDto.ToModel();
		return message;
	}

	// https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response
	public async Task CreateInteractionResponseAsync(
		ulong interactionId,
		string interactionToken,
		DiscordInteractionResponseArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"interactions/{interactionId}/{interactionToken}/callback";

		var argsDto = DiscordInteractionResponseArgsDto.FromModel(model: args);
		var payloadJson = _restClient.CreateJsonContent(value: argsDto);

		using var content = args.Data is DiscordInteractionCallbackMessage message
			? CreateContentForMessage(attachments: message.Attachments, files: message.Files, payloadJson: payloadJson)
			: payloadJson;

		// ReSharper disable once AccessToDisposedClosure
		await _restClient
			.SendRequestAsync(
				requestFactoryFunc: () =>
					new HttpRequestMessage(method: HttpMethod.Post, requestUri: url) { Content = content },
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	// https://discord.com/developers/docs/interactions/receiving-and-responding#delete-original-interaction-response
	public async Task DeleteOriginalInteractionResponseAsync(
		ulong applicationId,
		string interactionToken,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"webhooks/{applicationId}/{interactionToken}/messages/@original";

		// ReSharper disable once AccessToDisposedClosure
		await _restClient
			.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Delete, requestUri: url),
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	// https://discord.com/developers/docs/interactions/receiving-and-responding#edit-original-interaction-response
	public async Task<DiscordMessage> EditOriginalInteractionResponseAsync(
		ulong applicationId,
		string interactionToken,
		DiscordEditWebhookMessageArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"webhooks/{applicationId}/{interactionToken}/messages/@original";

		var argsDto = DiscordEditWebhookMessageArgsDto.FromModel(model: args);
		var payloadJson = _restClient.CreateJsonContent(value: argsDto);

		using var content = CreateContentForMessage(
			attachments: args.Attachments,
			files: args.Files,
			payloadJson: payloadJson
		);

		// ReSharper disable once AccessToDisposedClosure
		var messageDto = await _restClient
			.SendRequestAsync<DiscordMessageDto>(
				requestFactoryFunc: () =>
					new HttpRequestMessage(method: HttpMethod.Patch, requestUri: url) { Content = content },
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);

		var message = messageDto.ToModel();
		return message;
	}

	// https://discord.com/developers/docs/interactions/receiving-and-responding#get-original-interaction-response
	public async Task<DiscordMessage> GetOriginalInteractionResponseAsync(
		ulong applicationId,
		string interactionToken,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"webhooks/{applicationId}/{interactionToken}/messages/@original";

		var messageDto = await _restClient
			.SendRequestAsync<DiscordMessageDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: url),
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);

		var message = messageDto.ToModel();
		return message;
	}
}