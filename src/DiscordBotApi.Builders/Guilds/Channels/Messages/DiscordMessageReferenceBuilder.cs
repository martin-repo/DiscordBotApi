// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageReferenceBuilder.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Builders.Guilds.Channels.Messages;

public class DiscordMessageReferenceBuilder
{
	private ulong? _channelId;
	private bool? _failIfNotExists;
	private ulong? _guildId;
	private ulong? _messageId;

	public DiscordMessageReferenceBuilder WithChannelId(ulong? channelId)
	{
		_channelId = channelId;
		return this;
	}

	public DiscordMessageReferenceBuilder WithFailIfNotExists(bool? failIfNotExists)
	{
		_failIfNotExists = failIfNotExists;
		return this;
	}

	public DiscordMessageReferenceBuilder WithGuildId(ulong? guildId)
	{
		_guildId = guildId;
		return this;
	}

	public DiscordMessageReferenceBuilder WithMessageId(ulong? messageId)
	{
		_messageId = messageId;
		return this;
	}

	public DiscordMessageReference Build() =>
		new()
		{
			ChannelId = _channelId,
			FailIfNotExists = _failIfNotExists,
			GuildId = _guildId,
			MessageId = _messageId,
		};
}