// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageBase.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Emojis;

namespace DiscordBotApi.Models.Guilds.Channels.Messages;

public abstract record DiscordMessageBase
{
	internal DiscordMessageBase(DiscordBotClient botClient, string id, string channelId)
	{
		BotClient = botClient;

		Id = ulong.Parse(s: id);
		ChannelId = ulong.Parse(s: channelId);
	}

	public ulong ChannelId { get; init; }

	public ulong Id { get; init; }

	protected DiscordBotClient BotClient { get; }

	public async Task CreateReactionAsync(DiscordEmoji emoji) =>
		await BotClient.CreateReactionAsync(channelId: ChannelId, messageId: Id, emoji: emoji)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task DeleteAllReactionsForEmojiAsync(DiscordEmoji emoji) =>
		await BotClient.DeleteAllReactionsForEmojiAsync(channelId: ChannelId, messageId: Id, emoji: emoji)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task DeleteAsync() =>
		await BotClient.DeleteMessageAsync(channelId: ChannelId, messageId: Id)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task<DiscordMessage> EditAsync(DiscordEditMessageArgs args) =>
		await BotClient.EditMessageAsync(channelId: ChannelId, messageId: Id, args: args)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task PinAsync() =>
		await BotClient.PinMessageAsync(channelId: ChannelId, messageId: Id)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task<DiscordChannel> StartThreadAsync(DiscordStartThreadWithMessageArgs args) =>
		await BotClient.StartThreadWithMessageAsync(channelId: ChannelId, messageId: Id, args: args)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task UnpinAsync() =>
		await BotClient.UnpinMessageAsync(channelId: ChannelId, messageId: Id)
			.ConfigureAwait(continueOnCapturedContext: false);
}