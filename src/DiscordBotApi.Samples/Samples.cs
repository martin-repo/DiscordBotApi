// -------------------------------------------------------------------------------------------------
// <copyright file="Samples.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Samples
{
    using System.Drawing;

    using DiscordBotApi.Models.Gateway;
    using DiscordBotApi.Models.Guilds.Channels;
    using DiscordBotApi.Models.Guilds.Channels.Messages;
    using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;
    using DiscordBotApi.Models.Rest;
    using DiscordBotApi.Rest;

    public class GettingStarted
    {
        public async Task ConnectingToGateway()
        {
            var botClient = new DiscordBotClient("[bot token]");

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

            await botClient.ConnectToGatewayAsync(gateway.Url, DiscordGatewayIntent.Guilds);
        }

        public async Task CreateMessage(ulong guildId, ulong channelId)
        {
            var botClient = new DiscordBotClient("[bot token]");

            _ = await botClient.CreateMessageAsync(channelId, new() { Content = "Hello" });

            var embed = new DiscordEmbed { Color = Color.Red, Title = "Title", Description = "Description" };
            _ = await botClient.CreateMessageAsync(channelId, new() { Embeds = new[] { embed } });

            // Alternatively, you can also work via a channel object
            var channels = await botClient.GetGuildChannelsAsync(guildId);
            var channel = channels.First(c => c.Type == DiscordChannelType.GuildText);
            _ = await channel.CreateMessageAsync(new() { Content = "Hello" });
        }

        public void HandleGatewayExceptions()
        {
            var botClient = new DiscordBotClient("[bot token]");

            botClient.GatewayDisconnected += (_, eventArgs) =>
            {
                var (type, description) = eventArgs;

                // DiscordBotApi will attempt to keep the gateway connection
                // up as much as possible. But in rare occations it may fail.
                // Handle when gateway is disconnected.
                // ...
            };

            botClient.GatewayException += (_, gatewayException) =>
            {
                // Handle when there was an unhandled exception in the gateway logic.
                // ...
            };
        }

        public void HandleRateLimitExceeded()
        {
            var botClient = new DiscordBotClient("[bot token]");

            // DiscordBotApi will manage rate limitations and will
            // automatically retry failed calls.
            botClient.RestRateLimitExceeded += (_, args) =>
            {
                // But sometimes, for example when uploading Emojis, you may hit
                // an unusually long wait duration that you may wish to abort.
                // (After uploading 50 emojis you are rate limited for 60 minutes.)
                if (args.RateLimitResponse.RetryAfter > TimeSpan.FromSeconds(20))
                {
                    // Cancelling will cause an OperationCancelledException to be thrown
                    // on the thread that is making the call to Discord.
                    args.Cancel();
                }
            };
        }

        public async Task HandleRestExceptions()
        {
            var botClient = new DiscordBotClient("[bot token]");

            DiscordMessage message;
            try
            {
                message = await botClient.GetChannelMessageAsync(123, 123);
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
}