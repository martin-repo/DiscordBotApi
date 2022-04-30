// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientInteraction.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi
{
    using System.Net;

    using DiscordBotApi.Models.Interactions;

    public partial class DiscordBotClient
    {
        // https://discord.com/developers/docs/interactions/receiving-and-responding#create-interaction-response
        public async Task CreateInteractionResponseAsync(ulong interactionId, string interactionToken, DiscordInteractionResponseArgs args)
        {
            var url = $"interactions/{interactionId}/{interactionToken}/callback";

            var argsDto = new DiscordInteractionResponseArgsDto(args);
            await _restClient.SendRequestAsync(
                                 () =>
                                 {
                                     var request = new HttpRequestMessage(HttpMethod.Post, url);
                                     request.Content = _restClient.CreateJsonContent(argsDto);
                                     return request;
                                 },
                                 HttpStatusCode.NoContent)
                             .ConfigureAwait(false);
        }
    }
}