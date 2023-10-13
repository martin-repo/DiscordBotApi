// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordChannel.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Channels.Messages;

namespace DiscordBotApi.Models.Guilds.Channels;

// https://discord.com/developers/docs/resources/channel#channel-object
public record DiscordChannel
{
	private readonly DiscordBotClient _botClient;

	internal DiscordChannel(DiscordBotClient botClient, DiscordChannelDto dto)
	{
		_botClient = botClient;

		Id = ulong.Parse(s: dto.Id);
		Type = (DiscordChannelType)dto.Type;
		GuildId = dto.GuildId is not null
			? ulong.Parse(s: dto.GuildId)
			: null;
		Position = dto.Position;
		PermissionOverwrites = dto.PermissionOverwrites?.Select(selector: o => new DiscordPermissionOverwrite(dto: o))
			.ToArray();
		Name = dto.Name;
		Topic = dto.Topic;
		ParentId = dto.ParentId is not null
			? ulong.Parse(s: dto.ParentId)
			: null;
		ThreadMetadata = dto.ThreadMetadata is not null
			? new DiscordThreadMetadata(dto: dto.ThreadMetadata)
			: null;
		AvailableTags = dto.AvailableTags?.Select(selector: t => new DiscordForumTag(dto: t))
			.ToArray();
		DefaultAutoArchiveDuration = dto.DefaultAutoArchiveDuration;
		DefaultReactionEmoji = dto.DefaultReactionEmoji is not null
			? new DiscordDefaultReaction(dto: dto.DefaultReactionEmoji)
			: null;
		DefaultSortOrder = dto.DefaultSortOrder is not null
			? (DiscordSortOrderType)dto.DefaultSortOrder
			: null;
		DefaultForumLayout = dto.DefaultForumLayout is not null
			? (DiscordForumLayoutType)dto.DefaultForumLayout
			: null;
	}

	public IReadOnlyCollection<DiscordForumTag>? AvailableTags { get; init; }

	public int? DefaultAutoArchiveDuration { get; init; }

	public DiscordForumLayoutType? DefaultForumLayout { get; init; }

	public DiscordDefaultReaction? DefaultReactionEmoji { get; init; }

	public DiscordSortOrderType? DefaultSortOrder { get; init; }

	public ulong? GuildId { get; init; }

	public ulong Id { get; init; }

	public string? Name { get; init; }

	public ulong? ParentId { get; init; }

	public IReadOnlyCollection<DiscordPermissionOverwrite>? PermissionOverwrites { get; init; }

	public int? Position { get; init; }

	public DiscordThreadMetadata? ThreadMetadata { get; init; }

	public string? Topic { get; init; }

	public DiscordChannelType Type { get; init; }

	public async Task BulkDeleteMessagesAsync(DiscordBulkDeleteMessagesArgs args) =>
		await _botClient.BulkDeleteMessagesAsync(channelId: Id, args: args)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task<DiscordMessage> CreateMessageAsync(DiscordCreateMessageArgs args) =>
		await _botClient.CreateMessageAsync(channelId: Id, args: args)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task DeleteOrCloseAsync() =>
		await _botClient.DeleteOrCloseChannelAsync(channelId: Id)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task<DiscordMessage> GetMessageAsync(ulong messageId) =>
		await _botClient.GetChannelMessageAsync(channelId: Id, messageId: messageId)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task<IReadOnlyCollection<DiscordMessage>> GetMessagesAsync(DiscordGetChannelMessagesArgs? args = null) =>
		await _botClient.GetChannelMessagesAsync(channelId: Id, args: args)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task<IReadOnlyCollection<DiscordMessage>> GetPinnedMessagesAsync() =>
		await _botClient.GetPinnedMessagesAsync(channelId: Id)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task<DiscordThreadResponse> ListPublicArchivedThreadsAsync(DiscordListPublicArchivedThreadsArgs? args = null) =>
		await _botClient.ListPublicArchivedThreadsAsync(channelId: Id, args: args)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task<DiscordChannel> ModifyAsync(DiscordModifyGuildChannelArgs args) =>
		await _botClient.ModifyChannelAsync(channelId: Id, args: args)
			.ConfigureAwait(continueOnCapturedContext: false);

	public async Task<DiscordChannel> ModifyThreadAsync(DiscordModifyThreadArgs args) =>
		await _botClient.ModifyThreadAsync(channelId: Id, args: args)
			.ConfigureAwait(continueOnCapturedContext: false);
}