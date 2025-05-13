// -------------------------------------------------------------------------------------------------
// <copyright file="IDiscordBotClient.Application.cs" company="Martin Bergablod">
//   Copyright (c) 2024 Martin Bergablod. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using DiscordBotApi.Interface.Models.Applications;

namespace DiscordBotApi.Interface;

public partial interface IDiscordBotClient
{
	Task<DiscordApplicationCommand> CreateGuildApplicationCommandAsync(
		ulong applicationId,
		ulong guildId,
		DiscordCreateGuildApplicationCommandArgs args,
		CancellationToken cancellationToken = default
	);
}