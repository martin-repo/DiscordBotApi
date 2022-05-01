// -------------------------------------------------------------------------------------------------
// <copyright file="CategoryTests.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.EndToEndTests.Guild
{
    using System.Threading.Tasks;

    using AutoFixture.Xunit2;

    using DiscordBotApi.Models.Guilds;
    using DiscordBotApi.Models.Guilds.Channels;

    using FluentAssertions;

    using Xunit;

    [Collection("DiscordBotClient collection")]
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

            void OnChannelCreate(object? sender, DiscordChannel eventArgs)
            {
                completion.SetResult(eventArgs);
            }

            void ValidateChannel(DiscordChannel channel)
            {
                channel.Name.Should().Be(name);
            }

            _client.ChannelCreate += OnChannelCreate;
            try
            {
                var channelFromRest = await _client.CreateGuildChannelAsync(
                                          _guildId,
                                          new DiscordCreateGuildChannelArgs { Name = name, Type = DiscordChannelType.GuildCategory });
                var channelFromGateway = await completion.Task;

                typeof(DiscordChannel).GetProperties().Length.Should().Be(9);
                ValidateChannel(channelFromRest);
                ValidateChannel(channelFromGateway);
            }
            finally
            {
                _client.ChannelCreate -= OnChannelCreate;
            }
        }
    }
}