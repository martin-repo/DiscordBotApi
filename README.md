# DiscordBotApi
DiscordBotApi is a minimalistic layer on top of the official Discord API.

This API is not complete, and is being expanded as needed. [Create an issue](https://github.com/kpop-fan/DiscordBotApi/issues/new/choose) if you need features from Discord API that are not yet implemented.

## Getting help

- [Official Discord API documentation](https://discord.com/developers/docs/intro)

## Getting started

- Install [DiscordBotApi NuGet](https://www.nuget.org/packages/DiscordBotApi/)
- Follow examples below

### Connecting to gateway
```cs
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
```

### Create and handle chat command
```cs
public async Task CreateCommandAsync(ulong applicationId, ulong guildId)
{
    var command = await _botClient.CreateGuildApplicationCommandAsync(
                      applicationId,
                      guildId,
                      new()
                      {
                          Name = "blep",
                          Description = "Send a random adorable animal photo",
                          Options = new DiscordApplicationCommandOption[]
                                    {
                                        new()
                                        {
                                            Name = "animal",
                                            Description = "The type of animal",
                                            Type = DiscordApplicationCommandOptionType.String,
                                            Required = true,
                                            Choices = new DiscordApplicationCommandOptionChoice[]
                                                      {
                                                          new() { Name = "Dog", Value = "animal_dog" },
                                                          new() { Name = "Cat", Value = "animal_cat" },
                                                          new() { Name = "Penguin", Value = "animal_penguin" }
                                                      }
                                        },
                                        new()
                                        {
                                            Name = "only_smol",
                                            Description = "Whether to show only baby animals",
                                            Type = DiscordApplicationCommandOptionType.Boolean,
                                            Required = false
                                        }
                                    }
                      });
}

public void HandleCommand()
{
    _botClient.InteractionCreate += async (_, interaction) =>
    {
        if (interaction.Type != DiscordInteractionType.ApplicationCommand)
        {
            // Not an application command
            return;
        }

        if (interaction.Data!.Type != DiscordApplicationCommandType.ChatInput)
        {
            // Not chat input
            return;
        }

        if (interaction.Data.Name != "blep")
        {
            // Not the command we're looking for
            return;
        }

        var isInvokedInGuild = interaction.Member != null;
        var user = isInvokedInGuild ? interaction.Member!.User! : interaction.User!;

        var animalOption = interaction.Data.Options!.First(o => o.Name == "animal");
        var animalValue = (string)animalOption.Value;

        await _botClient.CreateInteractionResponseAsync(
            interaction.Id,
            interaction.Token,
            new()
            {
                Type = DiscordInteractionCallbackType.ChannelMessageWithSource,
                Data = new DiscordInteractionCallbackMessage { Content = $"{user.Username}, you picked {animalValue}", Flags = DiscordMessageFlags.Ephemeral }
            });
    };
}
```

### Create message
```cs
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
```

### Create message with attachments
```cs
public async Task CreateMessageWithAttachments(ulong channelId, string[] filePaths)
{
    var botClient = new DiscordBotClient("[bot token]");

    var attachments = filePaths.Select((path, index) =>
        new DiscordMessageAttachment { Id = (ulong)index, Filename = Path.GetFileName(path) }).ToArray();
    var files = filePaths.Select((path, index) => new DiscordMessageFile { Id = (ulong)index, FilePath = path }).ToArray();
    var embed = new DiscordEmbed { Image = new DiscordImage { Url = $"attachment://{attachments.First().Filename}" } };
    var args = new DiscordCreateMessageArgs
               {
                   Content = "Hello",
                   Embeds = new[] { embed },
                   Attachments = attachments,
                   Files = files
               };
    _ = await botClient.CreateMessageAsync(channelId, args);
}
```

### Handle gateway exception
```cs
public void HandleGatewayExceptions()
{
    var botClient = new DiscordBotClient("[bot token]");

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
```

### Handle rate limit exception
```cs
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
```

### Handle REST exception
```cs
public async Task HandleRestExceptions(ulong channelId, ulong messageId)
{
    var botClient = new DiscordBotClient("[bot token]");

    DiscordMessage message;
    try
    {
        message = await botClient.GetChannelMessageAsync(channelId, messageId);
    }
    catch (DiscordRestException restException) when (restException.ErrorResponse.Code == DiscordJsonErrorCode.UnknownMessage)
    {
        // Handle when message doesn't exist
        // ...
    }

    // Handle when message exists
    // ...
}
```
