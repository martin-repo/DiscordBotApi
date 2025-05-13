// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuild.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Emojis;

namespace DiscordBotApi.Interface.Models.Guilds;

public sealed class DiscordGuild
{
	public int? ApproximateMemberCount { get; init; }

	public required IReadOnlyCollection<DiscordEmoji> Emojis { get; init; }

	public required ulong Id { get; init; }

	public int? MemberCount { get; init; }

	public IReadOnlyCollection<DiscordGuildMember>? Members { get; init; }

	public required string Name { get; init; }

	public required DiscordGuildPremiumTier PremiumTier { get; init; }

	public int? PremiumTierSubscriptionCount { get; init; }

	public required IReadOnlyCollection<DiscordRole> Roles { get; init; }
}