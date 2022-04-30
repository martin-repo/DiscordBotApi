// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageOverloads.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

    public partial record DiscordMessage
    {
        public async Task<DiscordMessage> EditAsync(string content)
        {
            return await _botClient.EditMessageAsync(ChannelId, Id, new DiscordEditMessageArgs { Content = content }).ConfigureAwait(false);
        }

        public async Task<DiscordMessage> EditAsync(DiscordEmbed embed)
        {
            return await _botClient.EditMessageAsync(ChannelId, Id, new DiscordEditMessageArgs { Embeds = new[] { embed } }).ConfigureAwait(false);
        }
    }
}