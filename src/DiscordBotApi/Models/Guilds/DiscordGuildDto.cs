// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Guilds.Emojis;

    internal record DiscordGuildDto(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("roles")] DiscordRoleDto[] Roles,
        [property: JsonPropertyName("emojis")] DiscordEmojiDto[] Emojis,
        [property: JsonPropertyName("premium_tier")] int PremiumTier,
        [property: JsonPropertyName("premium_subscription_count")] int? PremiumTierSubscriptionCount,
        [property: JsonPropertyName("approximate_member_count")] int? ApproximateMemberCount);
}