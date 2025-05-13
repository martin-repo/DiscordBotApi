// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordBotClient.Channel.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Channels;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

namespace DiscordBotApi.Interface;

public partial interface IDiscordBotClient
{
	Task<(DiscordChannel Channel, DiscordMessage Message)> CreateForumThreadAsync(
		ulong channelId,
		DiscordCreateForumThreadArgs args,
		CancellationToken cancellationToken = default
	);

	Task<DiscordMessage> CreateMessageAsync(
		ulong channelId,
		DiscordCreateMessageArgs args,
		CancellationToken cancellationToken = default
	);

	Task DeleteMessageAsync(ulong channelId, ulong messageId, CancellationToken cancellationToken = default);

	Task DeleteOrCloseChannelAsync(ulong channelId, CancellationToken cancellationToken = default);

	Task<DiscordMessage> EditMessageAsync(
		ulong channelId,
		ulong messageId,
		DiscordEditMessageArgs args,
		CancellationToken cancellationToken = default
	);

	Task<DiscordMessage> GetChannelMessageAsync(
		ulong channelId,
		ulong messageId,
		CancellationToken cancellationToken = default
	);

	Task<IReadOnlyCollection<DiscordMessage>> GetChannelMessagesAsync(
		ulong channelId,
		DiscordGetChannelMessagesArgs? args = null,
		CancellationToken cancellationToken = default
	);

	Task<DiscordChannel> ModifyChannelAsync(
		ulong channelId,
		DiscordModifyGuildChannelArgs args,
		CancellationToken cancellationToken = default
	);
}