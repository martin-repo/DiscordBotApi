// -------------------------------------------------------------------------------------------------
// <copyright file="RoleTests.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.EndToEndTests.Guild
{
    using System;
    using System.Threading.Tasks;

    using AutoFixture.Xunit2;

    using DiscordBotApi.Models.Gateway.Events;
    using DiscordBotApi.Models.Guilds;

    using FluentAssertions;

    using Xunit;

    [Collection("DiscordBotClient collection")]
    public class RoleTests
    {
        private readonly DiscordBotClient _client;
        private readonly ulong _guildId;

        public RoleTests(DiscordBotClientFixture fixture)
        {
            _client = fixture.BotClient;
            _guildId = fixture.GuildId;
        }

        [Theory]
        [AutoData]
        private async Task CreateGuildRoleAsync(string name, DiscordPermissions permissions)
        {
            var completion = TaskCompletionSourceCreator.Create<DiscordGuildRoleCreate>();

            void OnGuildRoleCreate(object? sender, DiscordGuildRoleCreate eventArgs)
            {
                completion.SetResult(eventArgs);
            }

            void ValidateRole(DiscordRole role)
            {
                role.Name.Should().Be(name);
                role.Permissions.Should().Be(permissions);
            }

            _client.GuildRoleCreate += OnGuildRoleCreate;
            try
            {
                var roleFromRest = await _client.CreateGuildRoleAsync(
                                       _guildId,
                                       new DiscordCreateGuildRoleArgs { Name = name, Permissions = permissions });
                var roleFromGateway = await completion.Task;

                typeof(DiscordRole).GetProperties().Length.Should().Be(3);
                ValidateRole(roleFromRest);
                ValidateRole(roleFromGateway.Role);
            }
            finally
            {
                _client.GuildRoleCreate -= OnGuildRoleCreate;
            }
        }

        [Fact]
        private async Task DeleteGuildRoleAsync()
        {
            var completion = TaskCompletionSourceCreator.Create<DiscordGuildRoleDelete>();

            void OnGuildRoleDelete(object? sender, DiscordGuildRoleDelete eventArgs)
            {
                completion.SetResult(eventArgs);
            }

            _client.GuildRoleDelete += OnGuildRoleDelete;
            try
            {
                var role = await _client.CreateGuildRoleAsync(
                               _guildId,
                               new DiscordCreateGuildRoleArgs { Name = Guid.NewGuid().ToString("D"), Permissions = DiscordPermissions.None });

                await role.DeleteAsync();
                var guildRole = await completion.Task;

                guildRole.GuildId.Should().Be(_guildId);
                guildRole.RoleId.Should().Be(role.Id);
            }
            finally
            {
                _client.GuildRoleDelete -= OnGuildRoleDelete;
            }
        }

        [Theory]
        [AutoData]
        private async Task ModifyGuildRoleAsync(string name, DiscordPermissions permissions)
        {
            var completion = TaskCompletionSourceCreator.Create<DiscordGuildRoleUpdate>();

            void OnGuildRoleUpdate(object? sender, DiscordGuildRoleUpdate eventArgs)
            {
                completion.SetResult(eventArgs);
            }

            void ValidateRole(DiscordRole role)
            {
                role.Name.Should().Be(name);
                role.Permissions.Should().Be(permissions);
            }

            _client.GuildRoleUpdate += OnGuildRoleUpdate;
            try
            {
                var roleFromRest = await _client.CreateGuildRoleAsync(
                                       _guildId,
                                       new DiscordCreateGuildRoleArgs { Name = Guid.NewGuid().ToString("D"), Permissions = DiscordPermissions.None });

                roleFromRest = await roleFromRest.ModifyAsync(new DiscordModifyGuildRoleArgs { Name = name, Permissions = permissions });
                var roleFromGateway = await completion.Task;

                typeof(DiscordRole).GetProperties().Length.Should().Be(3);
                ValidateRole(roleFromRest);
                ValidateRole(roleFromGateway.Role);
            }
            finally
            {
                _client.GuildRoleUpdate -= OnGuildRoleUpdate;
            }
        }
    }
}