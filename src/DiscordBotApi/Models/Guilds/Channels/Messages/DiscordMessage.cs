// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessage.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
    using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;
    using DiscordBotApi.Models.Guilds.Emojis;
    using DiscordBotApi.Models.Users;

    public partial record DiscordMessage
    {
        private readonly DiscordBotClient _botClient;

        internal DiscordMessage(DiscordBotClient botClient, DiscordMessageDto dto)
        {
            _botClient = botClient;

            Id = ulong.Parse(dto.Id);
            ChannelId = ulong.Parse(dto.ChannelId);
            GuildId = dto.GuildId != null ? ulong.Parse(dto.GuildId) : null;
            Author = new DiscordUser(dto.Author);
            Content = dto.Content;
            Timestamp = DateTime.Parse(dto.Timestamp);
            EditedTimestamp = dto.EditedTimestamp != null ? DateTime.Parse(dto.EditedTimestamp) : null;
            Attachments = dto.Attachments.Select(a => new DiscordMessageAttachment(a)).ToArray();
            Embeds = dto.Embeds.Select(e => new DiscordEmbed(e)).ToArray();
            Reactions = dto.Reactions?.Select(r => new DiscordReaction(r)).ToArray();
            Pinned = dto.Pinned;
            Thread = dto.Thread != null ? new DiscordChannel(botClient, dto.Thread) : null;
            Components = dto.Components?.Select(c => new DiscordMessageActionRow(c)).ToArray();
        }

        public IReadOnlyCollection<DiscordMessageAttachment> Attachments { get; init; }

        public DiscordUser Author { get; init; }

        public ulong ChannelId { get; init; }

        public IReadOnlyCollection<DiscordMessageActionRow>? Components { get; init; }

        public string Content { get; init; }

        public DateTime? EditedTimestamp { get; init; }

        public IReadOnlyCollection<DiscordEmbed> Embeds { get; init; }

        public ulong? GuildId { get; init; }

        public ulong Id { get; init; }

        public bool Pinned { get; init; }

        public IReadOnlyCollection<DiscordReaction>? Reactions { get; init; }

        public DiscordChannel? Thread { get; init; }

        public DateTime Timestamp { get; init; }

        public async Task CreateReactionAsync(DiscordEmoji emoji)
        {
            await _botClient.CreateReactionAsync(ChannelId, Id, emoji).ConfigureAwait(false);
        }

        public async Task DeleteAllReactionsForEmojiAsync(DiscordEmoji emoji)
        {
            await _botClient.DeleteAllReactionsForEmojiAsync(ChannelId, Id, emoji).ConfigureAwait(false);
        }

        public async Task DeleteAsync()
        {
            await _botClient.DeleteMessageAsync(ChannelId, Id).ConfigureAwait(false);
        }

        public async Task<DiscordMessage> EditAsync(DiscordEditMessageArgs args)
        {
            return await _botClient.EditMessageAsync(ChannelId, Id, args).ConfigureAwait(false);
        }

        public async Task PinAsync()
        {
            await _botClient.PinMessageAsync(ChannelId, Id).ConfigureAwait(false);
        }

        public async Task<DiscordChannel> StartThreadAsync(DiscordStartThreadWithMessageArgs args)
        {
            return await _botClient.StartThreadWithMessageAsync(ChannelId, Id, args).ConfigureAwait(false);
        }

        public async Task UnpinAsync()
        {
            await _botClient.UnpinMessageAsync(ChannelId, Id).ConfigureAwait(false);
        }
    }
}