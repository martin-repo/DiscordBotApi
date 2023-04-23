// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageBase.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using DiscordBotApi.Models.Guilds.Emojis;

    public abstract record DiscordMessageBase
    {
        internal DiscordMessageBase(DiscordBotClient botClient, string id, string channelId)
        {
            BotClient = botClient;

            Id = ulong.Parse(id);
            ChannelId = ulong.Parse(channelId);
        }

        public ulong ChannelId { get; init; }

        public ulong Id { get; init; }

        protected DiscordBotClient BotClient { get; }

        public async Task CreateReactionAsync(DiscordEmoji emoji)
        {
            await BotClient.CreateReactionAsync(ChannelId, Id, emoji).ConfigureAwait(false);
        }

        public async Task DeleteAllReactionsForEmojiAsync(DiscordEmoji emoji)
        {
            await BotClient.DeleteAllReactionsForEmojiAsync(ChannelId, Id, emoji).ConfigureAwait(false);
        }

        public async Task DeleteAsync()
        {
            await BotClient.DeleteMessageAsync(ChannelId, Id).ConfigureAwait(false);
        }

        public async Task<DiscordMessage> EditAsync(DiscordEditMessageArgs args)
        {
            return await BotClient.EditMessageAsync(ChannelId, Id, args).ConfigureAwait(false);
        }

        public async Task PinAsync()
        {
            await BotClient.PinMessageAsync(ChannelId, Id).ConfigureAwait(false);
        }

        public async Task<DiscordChannel> StartThreadAsync(DiscordStartThreadWithMessageArgs args)
        {
            return await BotClient.StartThreadWithMessageAsync(ChannelId, Id, args).ConfigureAwait(false);
        }

        public async Task UnpinAsync()
        {
            await BotClient.UnpinMessageAsync(ChannelId, Id).ConfigureAwait(false);
        }
    }
}