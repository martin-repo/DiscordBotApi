// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordChannel.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    using DiscordBotApi.Models.Guilds.Channels.Messages;

    public partial record DiscordChannel
    {
        private readonly DiscordBotClient _botClient;

        internal DiscordChannel(DiscordBotClient botClient, DiscordChannelDto dto)
        {
            _botClient = botClient;

            Id = ulong.Parse(dto.Id);
            Type = (DiscordChannelType)dto.Type;
            GuildId = dto.GuildId != null ? ulong.Parse(dto.GuildId) : null;
            Position = dto.Position;
            PermissionOverwrites = dto.PermissionOverwrites?.Select(o => new DiscordPermissionOverwrite(o)).ToArray();
            Name = dto.Name;
            Topic = dto.Topic;
            ParentId = dto.ParentId != null ? ulong.Parse(dto.ParentId) : null;
            ThreadMetadata = dto.ThreadMetadata != null ? new DiscordThreadMetadata(dto.ThreadMetadata) : null;
        }

        public ulong? GuildId { get; init; }

        public ulong Id { get; init; }

        public string? Name { get; init; }

        public ulong? ParentId { get; init; }

        public IReadOnlyCollection<DiscordPermissionOverwrite>? PermissionOverwrites { get; init; }

        public int? Position { get; init; }

        public DiscordThreadMetadata? ThreadMetadata { get; init; }

        public string? Topic { get; init; }

        public DiscordChannelType Type { get; init; }

        public async Task BulkDeleteMessagesAsync(DiscordBulkDeleteMessagesArgs args)
        {
            await _botClient.BulkDeleteMessagesAsync(Id, args).ConfigureAwait(false);
        }

        public async Task<DiscordMessage> CreateMessageAsync(DiscordCreateMessageArgs args)
        {
            return await _botClient.CreateMessageAsync(Id, args).ConfigureAwait(false);
        }

        public async Task DeleteOrCloseAsync()
        {
            await _botClient.DeleteOrCloseChannelAsync(Id).ConfigureAwait(false);
        }

        public async Task<DiscordMessage> GetMessageAsync(ulong messageId)
        {
            return await _botClient.GetChannelMessageAsync(Id, messageId).ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<DiscordMessage>> GetMessagesAsync(DiscordGetChannelMessagesArgs? args = null)
        {
            return await _botClient.GetChannelMessagesAsync(Id, args).ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<DiscordMessage>> GetPinnedMessagesAsync()
        {
            return await _botClient.GetPinnedMessagesAsync(Id).ConfigureAwait(false);
        }

        public async Task<DiscordThreadResponse> ListPublicArchivedThreadsAsync(DiscordListPublicArchivedThreadsArgs? args = null)
        {
            return await _botClient.ListPublicArchivedThreadsAsync(Id, args).ConfigureAwait(false);
        }

        public async Task<DiscordChannel> ModifyAsync(DiscordModifyGuildChannelArgs args)
        {
            return await _botClient.ModifyChannelAsync(Id, args).ConfigureAwait(false);
        }

        public async Task<DiscordChannel> ModifyThreadAsync(DiscordModifyThreadArgs args)
        {
            return await _botClient.ModifyThreadAsync(Id, args).ConfigureAwait(false);
        }
    }
}