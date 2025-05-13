// -------------------------------------------------------------------------------------------------
// <copyright file="CategoryTests.cs" company="kpop.fan">
//   Copyright (c) 2023 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Threading.Tasks;

using AutoFixture.Xunit2;

using DiscordBotApi.Interface.Models.Guilds;
using DiscordBotApi.Interface.Models.Guilds.Channels;

using FluentAssertions;

using Xunit;

namespace DiscordBotApi.EndToEndTests.Guild;

[Collection(name: "DiscordBotClient collection")]
public class CategoryTests
{
	private readonly DiscordBotClient _client;
	private readonly ulong _guildId;

	public CategoryTests(DiscordBotClientFixture fixture)
	{
		_client = fixture.BotClient;
		_guildId = fixture.GuildId;
	}

	[Theory]
	[AutoData]
	private async Task CreateGuildRoleAsync(string name)
	{
		var completion = TaskCompletionSourceCreator.Create<DiscordChannel>();

		void OnChannelCreate(object? sender, DiscordChannel eventArgs) => completion.SetResult(result: eventArgs);

		void ValidateChannel(DiscordChannel channel) =>
			channel.Name.Should()
				.Be(expected: name);

		_client.ChannelCreate += OnChannelCreate;
		try
		{
			var channelFromRest = await _client.CreateGuildChannelAsync(
				guildId: _guildId,
				args: new DiscordCreateGuildChannelArgs
				{
					Name = name,
					Type = DiscordChannelType.GuildCategory
				});
			var channelFromGateway = await completion.Task;

			typeof(DiscordChannel).GetProperties()
				.Length.Should()
				.Be(expected: 9);
			ValidateChannel(channel: channelFromRest);
			ValidateChannel(channel: channelFromGateway);
		}
		finally
		{
			_client.ChannelCreate -= OnChannelCreate;
		}
	}
}