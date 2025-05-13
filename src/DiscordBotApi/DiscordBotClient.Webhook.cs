// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientWebhook.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Net;

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Interface.Models.Webhooks;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Webhooks;
using DiscordBotApi.Rest;

namespace DiscordBotApi;

public partial class DiscordBotClient
{
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

		var argsDto = DiscordExecuteWebhookArgsDto.FromModel(model: args);
		var payloadJson = _restClient.CreateJsonContent(value: argsDto);

		using var content = CreateContentForMessage(
			attachments: args.Attachments,
			files: args.Files,
			payloadJson: payloadJson
		);

		if (wait == true)
		{
			// ReSharper disable once AccessToDisposedClosure
			var messageDto = await _restClient
				.SendRequestAsync<DiscordMessageDto>(
					requestFactoryFunc: () =>
						new HttpRequestMessage(method: HttpMethod.Post, requestUri: builder.ToString()) { Content = content },
					cancellationToken: cancellationToken
				)
				.ConfigureAwait(continueOnCapturedContext: false);

			var message = messageDto.ToModel();
			return message;
		}

		// ReSharper disable once AccessToDisposedClosure
		await _restClient
			.SendRequestAsync(
				requestFactoryFunc: () =>
					new HttpRequestMessage(method: HttpMethod.Post, requestUri: builder.ToString()) { Content = content },
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);
		return null;
	}
}