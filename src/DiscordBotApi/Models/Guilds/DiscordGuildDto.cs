// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildDto.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Interface.Models.Guilds;
using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds;

// https://discord.com/developers/docs/resources/guild#guild-object-guild-structure
internal sealed record DiscordGuildDto(
	[property: JsonPropertyName(name: "id")]
	string Id,
	[property: JsonPropertyName(name: "name")]
	string Name,
	[property: JsonPropertyName(name: "roles")]
	DiscordRoleDto[] Roles,
	[property: JsonPropertyName(name: "emojis")]
	DiscordEmojiDto[] Emojis,
	[property: JsonPropertyName(name: "member_count")]
	int? MemberCount,
	[property: JsonPropertyName(name: "members")]
	DiscordGuildMemberDto[]? Members,
	[property: JsonPropertyName(name: "premium_tier")]
	int PremiumTier,
	[property: JsonPropertyName(name: "premium_subscription_count")]
	int? PremiumTierSubscriptionCount,
	[property: JsonPropertyName(name: "approximate_member_count")]
	int? ApproximateMemberCount
)
{
	public DiscordGuild ToModel() =>
		new()
		{
			Id = ulong.Parse(s: Id),
			Name = Name,
			Roles = Roles.Select(selector: r => r.ToModel()).ToArray(),
			Emojis = Emojis.Select(selector: e => e.ToModel()).ToArray(),
			MemberCount = MemberCount,
			Members = Members?.Select(selector: m => m.ToModel()).ToArray(),
			PremiumTier = (DiscordGuildPremiumTier)PremiumTier,
			PremiumTierSubscriptionCount = PremiumTierSubscriptionCount,
			ApproximateMemberCount = ApproximateMemberCount
		};
}