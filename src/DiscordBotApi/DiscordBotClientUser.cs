// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientUser.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Channels;
using DiscordBotApi.Models.Guilds.Channels.Messages;

namespace DiscordBotApi;

public partial class DiscordBotClient
{
	public async Task<DiscordChannel> CreateDmAsync(DiscordCreateDmArgs args, CancellationToken cancellationToken = default)
	{
		const string Url = "users/@me/channels";

		var argsDto = new DiscordCreateDmArgsDto(model: args);
		var channelDto = await _restClient.SendRequestAsync<DiscordChannelDto>(
				requestFactoryFunc: () =>
				{
					var request = new HttpRequestMessage(method: HttpMethod.Post, requestUri: Url);
					request.Content = _restClient.CreateJsonContent(value: argsDto);
					return request;
				},
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var channel = new DiscordChannel(botClient: this, dto: channelDto);
		return channel;
	}
}