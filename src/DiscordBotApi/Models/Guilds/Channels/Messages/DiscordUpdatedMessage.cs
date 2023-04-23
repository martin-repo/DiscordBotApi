// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordUpdatedMessage.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
    using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;
    using DiscordBotApi.Models.Users;

    public record DiscordUpdatedMessage : DiscordMessageBase
    {
        internal DiscordUpdatedMessage(DiscordBotClient botClient, DiscordUpdatedMessageDto dto)
            : base(botClient, dto.Id, dto.ChannelId)
        {
            GuildId = dto.GuildId != null ? ulong.Parse(dto.GuildId) : null;
            Author = dto.Author != null ? new DiscordUser(dto.Author) : null;
            Content = dto.Content;
            Timestamp = dto.Timestamp != null ? DateTime.Parse(dto.Timestamp) : null;
            EditedTimestamp = dto.EditedTimestamp != null ? DateTime.Parse(dto.EditedTimestamp) : null;
            Attachments = dto.Attachments?.Select(a => new DiscordMessageAttachment(a)).ToArray();
            Embeds = dto.Embeds?.Select(e => new DiscordEmbed(e)).ToArray();
            Reactions = dto.Reactions?.Select(r => new DiscordReaction(r)).ToArray();
            Pinned = dto.Pinned;
            Thread = dto.Thread != null ? new DiscordChannel(botClient, dto.Thread) : null;
            Components = dto.Components?.Select(c => new DiscordMessageActionRow(c)).ToArray();
        }

        public IReadOnlyCollection<DiscordMessageAttachment>? Attachments { get; init; }

        public DiscordUser? Author { get; init; }

        public IReadOnlyCollection<DiscordMessageActionRow>? Components { get; init; }

        public string? Content { get; init; }

        public DateTime? EditedTimestamp { get; init; }

        public IReadOnlyCollection<DiscordEmbed>? Embeds { get; init; }

        public ulong? GuildId { get; init; }

        public bool? Pinned { get; init; }

        public IReadOnlyCollection<DiscordReaction>? Reactions { get; init; }

        public DiscordChannel? Thread { get; init; }

        public DateTime? Timestamp { get; init; }
    }
}