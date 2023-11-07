// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBulkDeleteMessagesArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages;

public class DiscordBulkDeleteMessagesArgsBuilder
{
	private readonly List<ulong> _messages = new();

	public DiscordBulkDeleteMessagesArgsBuilder AddMessage(ulong message)
	{
		_messages.Add(item: message);
		return this;
	}

	public DiscordBulkDeleteMessagesArgs Build() =>
		new()
		{
			Messages = _messages.ToImmutableArray(),
		};
}