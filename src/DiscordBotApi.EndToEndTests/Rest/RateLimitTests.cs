// -------------------------------------------------------------------------------------------------
// <copyright file="RateLimitTests.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoFixture.Xunit2;

using DiscordBotApi.Models.Gateway.Events;
using DiscordBotApi.Models.Guilds;
using DiscordBotApi.Models.Guilds.Channels;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Guilds.Emojis;
using DiscordBotApi.Rest;

using FluentAssertions;

using Xunit;

namespace DiscordBotApi.EndToEndTests.Rest;

[Collection(name: "DiscordBotClient collection")]
public class RateLimitTests
{
	private readonly DiscordBotClient _client;
	private readonly ulong _guildId;

	public RateLimitTests(DiscordBotClientFixture fixture)
	{
		_client = fixture.BotClient;
		_guildId = fixture.GuildId;
	}

	[Theory]
	[AutoData]
	private async Task BulkDeleteMessageAsync(string categoryName, string channelName, string messageContent)
	{
		var category = await _client.CreateGuildChannelAsync(
			guildId: _guildId,
			args: new DiscordCreateGuildChannelArgs
			{
				Name = categoryName,
				Type = DiscordChannelType.GuildCategory
			});
		var channel = await _client.CreateGuildChannelAsync(
			guildId: _guildId,
			args: new DiscordCreateGuildChannelArgs
			{
				Name = channelName,
				Type = DiscordChannelType.GuildText,
				ParentId = category.Id
			});

		for (var i = 0; i < 20; i++)
		{
			await channel.CreateMessageAsync(args: new DiscordCreateMessageArgs { Content = messageContent });
		}

		var rateLimitHit = false;

		void OnRestRateLimitHit(object? sender, DiscordRateLimitExceeded eventArgs) => rateLimitHit = true;

		_client.RestRateLimitExceeded += OnRestRateLimitHit;
		try
		{
			var messages = await channel.GetMessagesAsync();
			await channel.BulkDeleteMessagesAsync(
				args: new DiscordBulkDeleteMessagesArgs
				{
					Messages = messages.Select(selector: m => m.Id)
						.ToArray()
				});
		}
		finally
		{
			_client.RestRateLimitExceeded -= OnRestRateLimitHit;
		}

		rateLimitHit.Should()
			.BeFalse();
	}

	[Fact]
	private async Task BulkEditMessagesAsync()
	{
		var category = await _client.CreateGuildChannelAsync(
			guildId: _guildId,
			args: new DiscordCreateGuildChannelArgs
			{
				Name = nameof(BulkEditMessagesAsync),
				Type = DiscordChannelType.GuildCategory
			});
		var channel = await _client.CreateGuildChannelAsync(
			guildId: _guildId,
			args: new DiscordCreateGuildChannelArgs
			{
				Name = Guid.NewGuid()
					.ToString(format: "D"),
				Type = DiscordChannelType.GuildText,
				ParentId = category.Id
			});

		var content = Guid.NewGuid()
			.ToString(format: "D");

		var messages = new List<DiscordMessage>();
		for (var i = 0; i < 15; i++)
		{
			messages.Add(item: await channel.CreateMessageAsync(args: new DiscordCreateMessageArgs { Content = content }));
		}

		var rateLimitHit = false;

		void OnRestRateLimitHit(object? sender, DiscordRateLimitExceeded eventArgs) => rateLimitHit = true;

		_client.RestRateLimitExceeded += OnRestRateLimitHit;
		try
		{
			for (var i = 0; i < 3; i++)
			{
				var li = i;
				var tasks = messages.Select(
					selector: (m, ti) => m.EditAsync(args: new DiscordEditMessageArgs { Content = $"{content}__{li}-{ti}" }));
				await Task.WhenAll(tasks: tasks);
			}
		}
		finally
		{
			_client.RestRateLimitExceeded -= OnRestRateLimitHit;
		}

		rateLimitHit.Should()
			.BeFalse();
	}

	[Fact]
	private async Task CreateMessageAsync()
	{
		var category = await _client.CreateGuildChannelAsync(
			guildId: _guildId,
			args: new DiscordCreateGuildChannelArgs
			{
				Name = nameof(CreateMessageAsync),
				Type = DiscordChannelType.GuildCategory
			});
		var channel = await _client.CreateGuildChannelAsync(
			guildId: _guildId,
			args: new DiscordCreateGuildChannelArgs
			{
				Name = Guid.NewGuid()
					.ToString(format: "D"),
				Type = DiscordChannelType.GuildText,
				ParentId = category.Id
			});

		var rateLimitHit = false;

		void OnRestRateLimitHit(object? sender, DiscordRateLimitExceeded eventArgs) => rateLimitHit = true;

		_client.RestRateLimitExceeded += OnRestRateLimitHit;
		try
		{
			for (var i = 0; i < 20; i++)
			{
				await channel.CreateMessageAsync(
					args: new DiscordCreateMessageArgs
					{
						Content = Guid.NewGuid()
							.ToString(format: "D")
					});
			}
		}
		finally
		{
			_client.RestRateLimitExceeded -= OnRestRateLimitHit;
		}

		rateLimitHit.Should()
			.BeFalse();
	}

	[Theory]
	[AutoData]
	private async Task EditMessageAsync(string categoryName, string channelName, string messageContent)
	{
		var category = await _client.CreateGuildChannelAsync(
			guildId: _guildId,
			args: new DiscordCreateGuildChannelArgs
			{
				Name = categoryName,
				Type = DiscordChannelType.GuildCategory
			});
		var channel = await _client.CreateGuildChannelAsync(
			guildId: _guildId,
			args: new DiscordCreateGuildChannelArgs
			{
				Name = channelName,
				Type = DiscordChannelType.GuildText,
				ParentId = category.Id
			});
		var message = await channel.CreateMessageAsync(args: new DiscordCreateMessageArgs { Content = messageContent });
		await message.CreateReactionAsync(emoji: new DiscordEmoji { Name = "🇬🇧" });
		await message.DeleteAllReactionsForEmojiAsync(emoji: new DiscordEmoji { Name = "🇬🇧" });
		await Task.Delay(millisecondsDelay: 500);

		var rateLimitHits = 0;

		void OnRestRateLimitHit(object? sender, DiscordRateLimitExceeded eventArgs) => rateLimitHits++;

		async Task OnMessageReactionAdd(DiscordMessageReactionAdd eventArgs)
		{
			var m = await _client.GetChannelMessageAsync(channelId: eventArgs.ChannelId, messageId: eventArgs.MessageId);
			await m.EditAsync(
				args: new DiscordEditMessageArgs
				{
					Content = Guid.NewGuid()
						.ToString()
				});
			await m.DeleteAllReactionsForEmojiAsync(emoji: eventArgs.Emoji);
		}

		async Task OnMessageReactionRemove(DiscordMessageReactionRemove eventArgs)
		{
			var m = await _client.GetChannelMessageAsync(channelId: eventArgs.ChannelId, messageId: eventArgs.MessageId);
			await m.EditAsync(
				args: new DiscordEditMessageArgs
				{
					Content = Guid.NewGuid()
						.ToString()
				});
			await m.DeleteAllReactionsForEmojiAsync(emoji: eventArgs.Emoji);
		}

		_client.RestRateLimitExceeded += OnRestRateLimitHit;
		_client.MessageReactionAdd += async (_, eventArgs) => await OnMessageReactionAdd(eventArgs: eventArgs);
		_client.MessageReactionRemove += async (_, eventArgs) => await OnMessageReactionRemove(eventArgs: eventArgs);
		try
		{
			for (var i = 0; i < 20; i++)
			{
				await message.CreateReactionAsync(emoji: new DiscordEmoji { Name = "🇬🇧" });
				await Task.Delay(millisecondsDelay: 500);
			}
		}
		finally
		{
			_client.RestRateLimitExceeded -= OnRestRateLimitHit;
		}

		rateLimitHits.Should()
			.Be(expected: 0);
	}
}