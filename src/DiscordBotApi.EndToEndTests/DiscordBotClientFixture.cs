// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientFixture.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.EndToEndTests
{
    using System;
    using System.Threading.Tasks;

    using DiscordBotApi.Models.Gateway;

    using Microsoft.Extensions.Configuration;

    using Serilog;

    using Xunit;

    public class DiscordBotClientFixture : IDisposable
    {
        public DiscordBotClientFixture()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                                                  .WriteTo.Debug(
                                                      outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                                                  .CreateLogger();
            Log.Logger.Information($"{nameof(DiscordBotClientFixture)} created");

            var configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                                                          .AddJsonFile("appsettings.json", false)
                                                          .AddJsonFile("appsettings.development.json", true)
                                                          .Build();
            var settingsSection = configuration.GetSection("Settings");

            GuildId = settingsSection.GetValue<ulong>("GuildId");
            BotRoleId = settingsSection.GetValue<ulong>("BotRoleId");

            var discordBotToken = settingsSection.GetValue<string>("DiscordBotToken");
            BotClient = new DiscordBotClient(discordBotToken, Log.Logger);

            CleanupAsync().Wait();

            var gatewayUrl = settingsSection.GetValue<string>("GatewayUrl");
            BotClient.ConnectToGatewayAsync(
                         gatewayUrl,
                         DiscordGatewayIntent.Guilds
                         | DiscordGatewayIntent.GuildMembers
                         | DiscordGatewayIntent.GuildMessages
                         | DiscordGatewayIntent.GuildMessageReactions)
                     .Wait();
        }

        public DiscordBotClient BotClient { get; }

        public ulong BotRoleId { get; }

        public ulong GuildId { get; }

        public void Dispose()
        {
            BotClient.DisposeAsync().AsTask().Wait();
        }

        private async Task CleanupAsync()
        {
            foreach (var role in await BotClient.GetGuildRolesAsync(GuildId))
            {
                if (role.Id == GuildId || role.Id == BotRoleId)
                {
                    // Ignore @everyone role
                    // Ignore bot role (should persist between test sessions)
                    continue;
                }

                await role.DeleteAsync();
            }

            foreach (var channel in await BotClient.GetGuildChannelsAsync(GuildId))
            {
                await channel.DeleteOrCloseAsync();
            }
        }
    }

    [CollectionDefinition("DiscordBotClient collection")]
    public class DiscordBotClientCollection : ICollectionFixture<DiscordBotClientFixture>
    {
    }
}