// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuild.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds;

public record DiscordGuild
{
	private readonly DiscordBotClient _botClient;

	internal DiscordGuild(DiscordBotClient botClient, DiscordGuildDto dto)
	{
		_botClient = botClient;

		var guildId = ulong.Parse(s: dto.Id);
		Id = guildId;
		Name = dto.Name;
		Roles = dto.Roles.Select(selector: r => new DiscordRole(botClient: botClient, guildId: guildId, dto: r))
			.ToArray();
		Emojis = dto.Emojis.Select(selector: e => new DiscordEmoji(dto: e))
			.ToArray();
		MemberCount = dto.MemberCount;
		Members = dto.Members?.Select(selector: m => new DiscordGuildMember(dto: m))
			.ToArray();
		PremiumTier = (DiscordGuildPremiumTier)dto.PremiumTier;
		PremiumTierSubscriptionCount = dto.PremiumTierSubscriptionCount;
		ApproximateMemberCount = dto.ApproximateMemberCount;
	}

	public int? ApproximateMemberCount { get; init; }

	public IReadOnlyCollection<DiscordEmoji> Emojis { get; init; }

	public ulong Id { get; init; }

	public int? MemberCount { get; init; }

	public IReadOnlyCollection<DiscordGuildMember>? Members { get; init; }

	public string Name { get; init; }

	public DiscordGuildPremiumTier PremiumTier { get; init; }

	public int? PremiumTierSubscriptionCount { get; init; }

	public IReadOnlyCollection<DiscordRole> Roles { get; init; }

	public async Task DeleteEmojiAsync(ulong emojiId) =>
		await _botClient.DeleteGuildEmojiAsync(guildId: Id, emojiId: emojiId)
			.ConfigureAwait(continueOnCapturedContext: false);
}