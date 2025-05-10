// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientFixture.cs" company="kpop.fan">
//   Copyright (c) 2023 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using DiscordBotApi.Interface.Models.Gateway;

using Microsoft.Extensions.Configuration;

using Serilog;

using Xunit;

namespace DiscordBotApi.EndToEndTests;

public class DiscordBotClientFixture : IDisposable
{
	public DiscordBotClientFixture()
	{
		Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
			.WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}")
			.CreateLogger();
		Log.Logger.Information(messageTemplate: $"{nameof(DiscordBotClientFixture)} created");

		var configuration = new ConfigurationBuilder().SetBasePath(basePath: AppContext.BaseDirectory)
			.AddJsonFile(path: "appsettings.json", optional: false)
			.AddJsonFile(path: "appsettings.development.json", optional: true)
			.Build();
		var settingsSection = configuration.GetSection(key: "Settings");

		GuildId = settingsSection.GetValue<ulong>(key: "GuildId");
		BotRoleId = settingsSection.GetValue<ulong>(key: "BotRoleId");

		var discordBotToken = settingsSection.GetValue<string>(key: "DiscordBotToken") ??
			throw new InvalidOperationException(message: "Invalid settings.");
		BotClient = new DiscordBotClient(botToken: discordBotToken, logger: Log.Logger);

		CleanupAsync()
			.Wait();

		var gatewayUrl = settingsSection.GetValue<string>(key: "GatewayUrl") ??
			throw new InvalidOperationException(message: "Invalid settings.");
		BotClient.ConnectToGatewayAsync(
				gatewayUrl: gatewayUrl,
				intents: DiscordGatewayIntent.Guilds |
				DiscordGatewayIntent.GuildMembers |
				DiscordGatewayIntent.GuildMessages |
				DiscordGatewayIntent.GuildMessageReactions)
			.Wait();
	}

	public DiscordBotClient BotClient { get; }

	public ulong BotRoleId { get; }

	public ulong GuildId { get; }

	public void Dispose() =>
		BotClient.DisposeAsync()
			.AsTask()
			.Wait();

	private async Task CleanupAsync()
	{
		foreach (var role in await BotClient.GetGuildRolesAsync(guildId: GuildId))
		{
			if (role.Id == GuildId ||
				role.Id == BotRoleId)
			{
				// Ignore @everyone role
				// Ignore bot role (should persist between test sessions)
				continue;
			}

			await role.DeleteAsync();
		}

		foreach (var channel in await BotClient.GetGuildChannelsAsync(guildId: GuildId))
		{
			await channel.DeleteOrCloseAsync();
		}
	}
}

[CollectionDefinition(name: "DiscordBotClient collection")]
public class DiscordBotClientCollection : ICollectionFixture<DiscordBotClientFixture>
{
}