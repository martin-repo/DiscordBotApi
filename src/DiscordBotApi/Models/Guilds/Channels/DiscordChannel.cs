// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordChannel.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Guilds.Channels.Messages;

namespace DiscordBotApi.Models.Guilds.Channels;

public record DiscordChannel
{
	private readonly DiscordBotClient _botClient;

	internal DiscordChannel(DiscordBotClient botClient, DiscordChannelDto dto)
	{
		_botClient = botClient;

		Id = ulong.Parse(s: dto.Id);
		Type = (DiscordChannelType)dto.Type;
		GuildId = dto.GuildId != null
			? ulong.Parse(s: dto.GuildId)
			: null;
		Position = dto.Position;
		PermissionOverwrites = dto.PermissionOverwrites?.Select(selector: o => new DiscordPermissionOverwrite(dto: o))
			.ToArray();
		Name = dto.Name;
		Topic = dto.Topic;
		ParentId = dto.ParentId != null
			? ulong.Parse(s: dto.ParentId)
			: null;
		ThreadMetadata = dto.ThreadMetadata != null
			? new DiscordThreadMetadata(dto: dto.ThreadMetadata)
			: null;
	}

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