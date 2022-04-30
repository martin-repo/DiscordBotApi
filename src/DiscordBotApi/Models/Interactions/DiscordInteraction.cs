// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteraction.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions
{
    using DiscordBotApi.Models.Guilds;
    using DiscordBotApi.Models.Guilds.Channels.Messages;
    using DiscordBotApi.Models.Users;

    public record DiscordInteraction
    {
        internal DiscordInteraction(DiscordBotClient botClient, DiscordInteractionDto dto)
        {
            BotClient = botClient;

            Id = ulong.Parse(dto.Id);
            ApplicationId = ulong.Parse(dto.ApplicationId);
            Type = (DiscordInteractionType)dto.Type;
            Data = dto.Data != null ? new DiscordInteractionData(dto.Data) : null;
            GuildId = dto.GuildId != null ? ulong.Parse(dto.GuildId) : null;
            ChannelId = dto.ChannelId != null ? ulong.Parse(dto.ChannelId) : null;
            Member = dto.Member != null ? new DiscordGuildMember(dto.Member) : null;
            User = dto.User != null ? new DiscordUser(dto.User) : null;
            Token = dto.Token;
            Message = dto.Message != null ? new DiscordMessage(botClient, dto.Message) : null;
        }

        public ulong ApplicationId { get; init; }

        public DiscordBotClient BotClient { get; }

        public ulong? ChannelId { get; init; }

        public DiscordInteractionData? Data { get; init; }

        public ulong? GuildId { get; init; }

        public ulong Id { get; init; }

        public DiscordGuildMember? Member { get; init; }

        public DiscordMessage? Message { get; init; }

        public string Token { get; init; }

        public DiscordInteractionType Type { get; init; }

        public DiscordUser? User { get; init; }

        public async Task CreateResponseAsync(DiscordInteractionCallbackType type, DiscordInteractionCallbackData? data = null)
        {
            await BotClient.CreateInteractionResponseAsync(Id, Token, new DiscordInteractionResponseArgs { Type = type, Data = data })
                           .ConfigureAwait(false);
        }
    }
}