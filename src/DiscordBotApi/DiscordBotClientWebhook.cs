// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientWebhook.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Net;

using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Webhooks;
using DiscordBotApi.Rest;

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

		var argsDto = new DiscordExecuteWebhookArgsDto(model: args);
		var payloadJson = _restClient.CreateJsonContent(value: argsDto);

		using var content = CreateContentForMessage(attachments: args.Attachments, files: args.Files, payloadJson: payloadJson);

		// ReSharper disable once AccessToDisposedClosure
		var messageDto = await _restClient.SendRequestAsync<DiscordMessageDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Post, requestUri: url) { Content = content },
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var message = new DiscordMessage(botClient: this, dto: messageDto);
		return message;
	}

	// https://discord.com/developers/docs/interactions/receiving-and-responding#edit-original-interaction-response
	public async Task<DiscordMessage> EditOriginalInteractionResponseAsync(
		ulong applicationId,
		string interactionToken,
		ulong original,
		DiscordEditWebhookMessageArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"webhooks/{applicationId}/{interactionToken}/messages/{original}";

		var argsDto = new DiscordEditWebhookMessageArgsDto(model: args);
		var payloadJson = _restClient.CreateJsonContent(value: argsDto);

		using var content = CreateContentForMessage(attachments: args.Attachments, files: args.Files, payloadJson: payloadJson);

		// ReSharper disable once AccessToDisposedClosure
		var messageDto = await _restClient.SendRequestAsync<DiscordMessageDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Patch, requestUri: url) { Content = content },
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var message = new DiscordMessage(botClient: this, dto: messageDto);
		return message;
	}

	// https://discord.com/developers/docs/resources/webhook#edit-webhook-message
	public async Task<DiscordMessage> EditWebhookMessageAsync(
		ulong webhookId,
		string webhookToken,
		ulong messageId,
		DiscordEditWebhookMessageArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"webhooks/{webhookId}/{webhookToken}/messages/{messageId}";

		var argsDto = new DiscordEditWebhookMessageArgsDto(model: args);
		var payloadJson = _restClient.CreateJsonContent(value: argsDto);

		using var content = CreateContentForMessage(attachments: args.Attachments, files: args.Files, payloadJson: payloadJson);

		// ReSharper disable once AccessToDisposedClosure
		var messageDto = await _restClient.SendRequestAsync<DiscordMessageDto>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Patch, requestUri: url) { Content = content },
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var message = new DiscordMessage(botClient: this, dto: messageDto);
		return message;
	}

	// https://discord.com/developers/docs/resources/webhook#execute-webhook
	public async Task<DiscordMessage?> ExecuteWebhookAsync(
		ulong webhookId,
		string webhookToken,
		bool? wait,
		DiscordExecuteWebhookArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var builder = new QueryBuilder(pathWithoutQuery: $"webhooks/{webhookId}/{webhookToken}");
		builder.Add(key: "wait", value: wait);

		var argsDto = new DiscordExecuteWebhookArgsDto(model: args);
		var payloadJson = _restClient.CreateJsonContent(value: argsDto);

		using var content = CreateContentForMessage(attachments: args.Attachments, files: args.Files, payloadJson: payloadJson);

		if (wait == true)
		{
			// ReSharper disable once AccessToDisposedClosure
			var messageDto = await _restClient.SendRequestAsync<DiscordMessageDto>(
					requestFactoryFunc: () =>
						new HttpRequestMessage(method: HttpMethod.Post, requestUri: builder.ToString()) { Content = content },
					cancellationToken: cancellationToken)
				.ConfigureAwait(continueOnCapturedContext: false);

			var message = new DiscordMessage(botClient: this, dto: messageDto);
			return message;
		}

		// ReSharper disable once AccessToDisposedClosure
		await _restClient.SendRequestAsync(
				requestFactoryFunc: () =>
					new HttpRequestMessage(method: HttpMethod.Post, requestUri: builder.ToString()) { Content = content },
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
		return null;
	}
}