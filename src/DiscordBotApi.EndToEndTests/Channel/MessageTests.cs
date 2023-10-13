// -------------------------------------------------------------------------------------------------
// <copyright file="MessageTests.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;

using AutoFixture.Xunit2;

using DiscordBotApi.Models.Guilds;
using DiscordBotApi.Models.Guilds.Channels;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

using FluentAssertions;

using Xunit;

namespace DiscordBotApi.EndToEndTests.Channel;

[Collection(name: "DiscordBotClient collection")]
public class MessageTests
{
	private readonly DiscordBotClient _client;
	private readonly ulong _guildId;

	public MessageTests(DiscordBotClientFixture fixture)
	{
		_client = fixture.BotClient;
		_guildId = fixture.GuildId;
	}

	[Theory]
	[AutoData]
	private async Task AttachmentAsync(string categoryName, string channelName)
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

		var attachments = new[]
		{
			new DiscordMessageAttachment
			{
				Id = 0,
				Filename = "file1.png"
			},
			new DiscordMessageAttachment
			{
				Id = 1,
				Filename = "file2.png"
			}
		};
		var files = new[]
		{
			new DiscordMessageFile
			{
				Id = 0,
				FilePath = "file1.png"
			},
			new DiscordMessageFile
			{
				Id = 1,
				FilePath = "file2.png"
			}
		};
		var createArgs = new DiscordCreateMessageArgs
		{
			Content = "Test",
			Attachments = attachments,
			Files = files
		};

		var createdMessage = await _client.CreateMessageAsync(channelId: channel.Id, args: createArgs);

		attachments = new[]
		{
			createdMessage.Attachments.First(),
			new DiscordMessageAttachment
			{
				Id = 0,
				Filename = "file3.png"
			}
		};
		files = new[]
		{
			new DiscordMessageFile
			{
				Id = 0,
				FilePath = "file3.png"
			}
		};
		var editArgs = new DiscordEditMessageArgs
		{
			Attachments = attachments,
			Files = files
		};

		var editedMessage = await _client.EditMessageAsync(channelId: channel.Id, messageId: createdMessage.Id, args: editArgs);
	}

	[Theory]
	[AutoData]
	private async Task AttachmentEmbed2Async(string categoryName, string channelName)
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

		var attachments = new[]
		{
			new DiscordMessageAttachment
			{
				Id = 0,
				Filename = "file1.png"
			},
			new DiscordMessageAttachment
			{
				Id = 1,
				Filename = "file2.png"
			}
		};
		var files = new[]
		{
			new DiscordMessageFile
			{
				Id = 0,
				FilePath = "file1.png"
			},
			new DiscordMessageFile
			{
				Id = 1,
				FilePath = "file2.png"
			}
		};
		var embeds = new[]
		{
			new DiscordEmbed
			{
				Description = "Test",
				Image = new DiscordImage { Url = "attachment://file1.png" }
			}
		};
		var createArgs = new DiscordCreateMessageArgs
		{
			Embeds = embeds,
			Attachments = attachments,
			Files = files
		};

		var createdMessage = await _client.CreateMessageAsync(channelId: channel.Id, args: createArgs);

		// Due to an issue in Discord api, attachments are "hidden" when they are "consumed" by an embed.
		createdMessage.Attachments.Count.Should()
			.Be(expected: 1);
	}

	[Theory]
	[AutoData]
	private async Task AttachmentEmbedAsync(string categoryName, string channelName)
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

		var attachments = new[]
		{
			new DiscordMessageAttachment
			{
				Id = 0,
				Filename = "file1.png"
			},
			new DiscordMessageAttachment
			{
				Id = 1,
				Filename = "file2.png"
			}
		};
		var files = new[]
		{
			new DiscordMessageFile
			{
				Id = 0,
				FilePath = "file1.png"
			},
			new DiscordMessageFile
			{
				Id = 1,
				FilePath = "file2.png"
			}
		};
		var embeds = new[]
		{
			new DiscordEmbed
			{
				Description = "Test",
				Thumbnail = new DiscordThumbnail { Url = "attachment://file1.png" },
				Image = new DiscordImage { Url = "attachment://file2.png" }
			}
		};
		var createArgs = new DiscordCreateMessageArgs
		{
			Embeds = embeds,
			Attachments = attachments,
			Files = files
		};

		var createdMessage = await _client.CreateMessageAsync(channelId: channel.Id, args: createArgs);

		// Due to an issue in Discord api, attachments are "hidden" when they are "consumed" by an embed.
		createdMessage.Attachments.Count.Should()
			.Be(expected: 0);
	}
}