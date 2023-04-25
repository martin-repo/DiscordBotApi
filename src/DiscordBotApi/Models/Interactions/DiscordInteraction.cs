// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteraction.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Users;

namespace DiscordBotApi.Models.Interactions;

public record DiscordInteraction
{
	internal DiscordInteraction(DiscordBotClient botClient, DiscordInteractionDto dto)
	{
		BotClient = botClient;

		Id = ulong.Parse(s: dto.Id);
		ApplicationId = ulong.Parse(s: dto.ApplicationId);
		Type = (DiscordInteractionType)dto.Type;
		Data = dto.Data != null
			? new DiscordInteractionData(dto: dto.Data)
			: null;
		GuildId = dto.GuildId != null
			? ulong.Parse(s: dto.GuildId)
			: null;
		ChannelId = dto.ChannelId != null
			? ulong.Parse(s: dto.ChannelId)
			: null;
		Member = dto.Member != null
			? new DiscordGuildMember(dto: dto.Member)
			: null;
		User = dto.User != null
			? new DiscordUser(dto: dto.User)
			: null;
		Token = dto.Token;
		Message = dto.Message != null
			? new DiscordMessage(botClient: botClient, dto: dto.Message)
			: null;
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

	public async Task CreateResponseAsync(DiscordInteractionCallbackType type, DiscordInteractionCallbackData? data = null) =>
		await BotClient.CreateInteractionResponseAsync(
				interactionId: Id,
				interactionToken: Token,
				args: new DiscordInteractionResponseArgs
				{
					Type = type,
					Data = data
				})
			.ConfigureAwait(continueOnCapturedContext: false);
}