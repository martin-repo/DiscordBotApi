// -------------------------------------------------------------------------------------------------
// <copyright file="Samples.cs" company="kpop.fan">
//   Copyright (c) 2023 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Drawing;

using DiscordBotApi.Interface.Models.Gateway;
using DiscordBotApi.Interface.Models.Guilds.Channels;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages;
using DiscordBotApi.Interface.Models.Guilds.Channels.Messages.Embeds;
using DiscordBotApi.Interface.Models.Rest;
using DiscordBotApi.Rest;

namespace DiscordBotApi.Samples;

public class GettingStarted
{
	public async Task ConnectingToGateway()
	{
		var botClient = new DiscordBotClient(botToken: "[bot token]");

		// Gateway.Url should be cached, eg. to disk or database
		var gateway = await botClient.GetGatewayAsync();

		// Connecting to gateway is only required for receiving events.
		// For sending messages, etc., there is no need to connect to the gateway.
		// Specify the events that should be handled
		// ...
		botClient.InteractionCreate += (_, interaction) =>
		{
			// Handle interaction
			// ...
		};

		await botClient.ConnectToGatewayAsync(gatewayUrl: gateway.Url, intents: DiscordGatewayIntent.Guilds);
	}

	public async Task CreateMessage(ulong guildId, ulong channelId)
	{
		var botClient = new DiscordBotClient(botToken: "[bot token]");

		_ = await botClient.CreateMessageAsync(channelId: channelId, args: new DiscordCreateMessageArgs { Content = "Hello" });

		var embed = new DiscordEmbed
		{
			Color = Color.Red,
			Title = "Title",
			Description = "Description"
		};
		_ = await botClient.CreateMessageAsync(
			channelId: channelId,
			args: new DiscordCreateMessageArgs { Embeds = new[] { embed } });

		// Alternatively, you can also work via a channel object
		var channels = await botClient.GetGuildChannelsAsync(guildId: guildId);
		var channel = channels.First(predicate: c => c.Type == DiscordChannelType.GuildText);
		_ = await channel.CreateMessageAsync(args: new DiscordCreateMessageArgs { Content = "Hello" });
	}

	public void HandleGatewayExceptions()
	{
		var botClient = new DiscordBotClient(botToken: "[bot token]");

		botClient.GatewayException += (_, gatewayException) =>
		{
			// DiscordBotApi will attempt to keep the gateway connection
			// up as much as possible. But in rare occations it may fail.

			if (gatewayException.IsDisconnected)
			{
				// Handle when gateway is disconnected.
				// ...
			}
		};
	}

	public void HandleRateLimitExceeded()
	{
		var botClient = new DiscordBotClient(botToken: "[bot token]");

		// DiscordBotApi will manage rate limitations and will
		// automatically retry failed calls.
		botClient.RestRateLimitExceeded += (_, args) =>
		{
			// But sometimes, for example when uploading Emojis, you may hit
			// an unusually long wait duration that you may wish to abort.
			// (After uploading 50 emojis you are rate limited for 60 minutes.)
			if (args.RateLimitResponse.RetryAfter > TimeSpan.FromSeconds(value: 20))
			{
				// Cancelling will cause an OperationCancelledException to be thrown
				// on the thread that is making the call to Discord.
				args.Cancel();
			}
		};
	}

	public async Task HandleRestExceptions()
	{
		var botClient = new DiscordBotClient(botToken: "[bot token]");

		DiscordMessage message;
		try
		{
			message = await botClient.GetChannelMessageAsync(channelId: 123, messageId: 123);
		}
		catch (DiscordRestException restException) when (restException.ErrorResponse.Code == DiscordJsonErrorCode.UnknownMessage)
		{
			// Handle when message doesn't exist
			// ...
		}

		// Handle when message exists
		// ...
	}
}