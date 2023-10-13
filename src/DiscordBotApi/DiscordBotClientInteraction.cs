// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientInteraction.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Net;

using DiscordBotApi.Models.Interactions;

namespace DiscordBotApi;

public partial class DiscordBotClient
{
	// https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response
	public async Task CreateInteractionResponseAsync(
		ulong interactionId,
		string interactionToken,
		DiscordInteractionResponseArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"interactions/{interactionId}/{interactionToken}/callback";

		var argsDto = new DiscordInteractionResponseArgsDto(model: args);
		var payloadJson = _restClient.CreateJsonContent(value: argsDto);

		using var content = args.Data is DiscordInteractionCallbackMessage message
			? CreateContentForMessage(attachments: message.Attachments, files: message.Files, payloadJson: payloadJson)
			: payloadJson;

		// ReSharper disable once AccessToDisposedClosure
		await _restClient.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Post, requestUri: url) { Content = content },
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}
}