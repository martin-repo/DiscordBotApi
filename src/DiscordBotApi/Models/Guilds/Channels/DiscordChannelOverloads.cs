// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordChannelOverloads.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    using DiscordBotApi.Models.Guilds.Channels.Messages;
    using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

    public partial record DiscordChannel
    {
        public async Task BulkDeleteMessagesAsync(ulong[] messages)
        {
            await _botClient.BulkDeleteMessagesAsync(Id, new DiscordBulkDeleteMessagesArgs { Messages = messages }).ConfigureAwait(false);
        }

        public async Task<DiscordMessage> CreateMessageAsync(string content)
        {
            return await _botClient.CreateMessageAsync(Id, new DiscordCreateMessageArgs { Content = content }).ConfigureAwait(false);
        }

        public async Task<DiscordMessage> CreateMessageAsync(DiscordEmbed embed)
        {
            return await _botClient.CreateMessageAsync(Id, new DiscordCreateMessageArgs { Embeds = new[] { embed } }).ConfigureAwait(false);
        }
    }
}