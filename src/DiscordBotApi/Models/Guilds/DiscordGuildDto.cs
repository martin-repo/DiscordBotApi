// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildDto.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds;

// https://discord.com/developers/docs/resources/guild#guild-object-guild-structure
internal record DiscordGuildDto(
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
);