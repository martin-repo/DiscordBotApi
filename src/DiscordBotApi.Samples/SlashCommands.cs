// -------------------------------------------------------------------------------------------------
// <copyright file="SlashCommands.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Models.Applications;
using DiscordBotApi.Models.Guilds.Channels.Messages;
using DiscordBotApi.Models.Interactions;

namespace DiscordBotApi.Samples;

public class SlashCommands
{
	private readonly DiscordBotClient _botClient;

	public SlashCommands(string botToken)
	{
		_botClient = new DiscordBotClient(botToken: botToken);
	}

	public async Task CreateCommandAsync(ulong applicationId, ulong guildId)
	{
		var command = await _botClient.CreateGuildApplicationCommandAsync(
			applicationId: applicationId,
			guildId: guildId,
			args: new DiscordCreateGuildApplicationCommandArgs
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
							new()
							{
								Name = "Dog",
								Value = "animal_dog"
							},
							new()
							{
								Name = "Cat",
								Value = "animal_cat"
							},
							new()
							{
								Name = "Penguin",
								Value = "animal_penguin"
							}
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

	public void HandleCommand() =>
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
			var user = isInvokedInGuild
				? interaction.Member!.User!
				: interaction.User!;

			var animalOption = interaction.Data.Options!.First(predicate: o => o.Name == "animal");
			var animalValue = (string)animalOption.Value;

			await _botClient.CreateInteractionResponseAsync(
				interactionId: interaction.Id,
				interactionToken: interaction.Token,
				args: new DiscordInteractionResponseArgs
				{
					Type = DiscordInteractionCallbackType.ChannelMessageWithSource,
					Data = new DiscordInteractionCallbackMessage
					{
						Content = $"{user.Username}, you picked {animalValue}",
						Flags = DiscordMessageFlags.Ephemeral
					}
				});
		};
}