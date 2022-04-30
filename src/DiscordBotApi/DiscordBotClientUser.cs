// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientUser.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi
{
    using DiscordBotApi.Models.Guilds.Channels;
    using DiscordBotApi.Models.Guilds.Channels.Messages;

    public partial class DiscordBotClient
    {
        public async Task<DiscordChannel> CreateDmAsync(DiscordCreateDmArgs args)
        {
            const string Url = "users/@me/channels";

            var argsDto = new DiscordCreateDmArgsDto(args);
            var channelDto = await _restClient.SendRequestAsync<DiscordChannelDto>(
                                                  () =>
                                                  {
                                                      var request = new HttpRequestMessage(HttpMethod.Post, Url);
                                                      request.Content = _restClient.CreateJsonContent(argsDto);
                                                      return request;
                                                  })
                                              .ConfigureAwait(false);

            var channel = new DiscordChannel(this, channelDto);
            return channel;
        }
    }
}