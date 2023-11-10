// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateDmArgsBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages;

public class DiscordCreateDmArgsBuilder
{
	private ulong _recipientId = default!;

	public DiscordCreateDmArgsBuilder WithRecipientId(ulong recipientId)
	{
		_recipientId = recipientId;
		return this;
	}

	public DiscordCreateDmArgs Build() =>
		new()
		{
			RecipientId = _recipientId,
		};
}