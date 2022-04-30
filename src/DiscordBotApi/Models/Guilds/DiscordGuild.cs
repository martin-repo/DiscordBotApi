// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuild.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    using DiscordBotApi.Models.Guilds.Emojis;

    public partial record DiscordGuild
    {
        private readonly DiscordBotClient _botClient;

        internal DiscordGuild(DiscordBotClient botClient, DiscordGuildDto dto)
        {
            _botClient = botClient;

            var guildId = ulong.Parse(dto.Id);
            Id = guildId;
            Name = dto.Name;
            Roles = dto.Roles.Select(r => new DiscordRole(botClient, guildId, r)).ToArray();
            Emojis = dto.Emojis.Select(e => new DiscordEmoji(e)).ToArray();
            PremiumTier = (DiscordGuildPremiumTier)dto.PremiumTier;
            PremiumTierSubscriptionCount = dto.PremiumTierSubscriptionCount;
            ApproximateMemberCount = dto.ApproximateMemberCount;
        }

        public int? ApproximateMemberCount { get; init; }

        public IReadOnlyCollection<DiscordEmoji> Emojis { get; init; }

        public ulong Id { get; init; }

        public string Name { get; init; }

        public DiscordGuildPremiumTier PremiumTier { get; init; }

        public int? PremiumTierSubscriptionCount { get; init; }

        public IReadOnlyCollection<DiscordRole> Roles { get; init; }

        public async Task DeleteEmojiAsync(ulong emojiId)
        {
            await _botClient.DeleteGuildEmojiAsync(Id, emojiId).ConfigureAwait(false);
        }
    }
}