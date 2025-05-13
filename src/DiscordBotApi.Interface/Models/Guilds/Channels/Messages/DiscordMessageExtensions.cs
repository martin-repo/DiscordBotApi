// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageExtensions.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Components;

namespace DiscordBotApi.Interface.Models.Guilds.Channels.Messages;

public static class DiscordMessageExtensions
{
	public static Task DeleteAsync(this DiscordMessage message, IDiscordBotClient botClient) =>
		botClient.DeleteMessageAsync(channelId: message.ChannelId, messageId: message.Id);

	public static Task<DiscordMessage> EditAsync(
		this DiscordMessage message,
		IDiscordBotClient botClient,
		string? content = null,
		IReadOnlyCollection<DiscordMessageComponent>? components = null
	) =>
		botClient.EditMessageAsync(
			channelId: message.ChannelId,
			messageId: message.Id,
			args: new DiscordEditMessageArgs
			{
				Content = content,
				Components = components
			}
		);
}