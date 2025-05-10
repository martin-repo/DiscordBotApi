// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientUser.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Channels;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Guilds.Channels;
using DiscordBotApi.Models.Guilds.Channels.Messages;

namespace DiscordBotApi;

public partial class DiscordBotClient
{
	public async Task<DiscordChannel> CreateDmAsync(
		DiscordCreateDmArgs args,
		CancellationToken cancellationToken = default
	)
	{
		const string Url = "users/@me/channels";

		var argsDto = DiscordCreateDmArgsDto.FromModel(model: args);
		var channelDto = await _restClient
			.SendRequestAsync<DiscordChannelDto>(
				requestFactoryFunc: () =>
				{
					var request = new HttpRequestMessage(method: HttpMethod.Post, requestUri: Url);
					request.Content = _restClient.CreateJsonContent(value: argsDto);
					return request;
				},
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);

		var channel = channelDto.ToModel();
		return channel;
	}
}