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
		await _restClient.SendRequestAsync(
				requestFactoryFunc: () =>
				{
					var request = new HttpRequestMessage(method: HttpMethod.Post, requestUri: url);
					request.Content = _restClient.CreateJsonContent(value: argsDto);
					return request;
				},
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}
}