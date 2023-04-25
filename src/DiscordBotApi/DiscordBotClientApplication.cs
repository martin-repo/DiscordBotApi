// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientApplication.cs" company="Martin Karlsson">
//   Copyright (c) 2023 Martin Karlsson. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Net;

using DiscordBotApi.Models.Applications;
using DiscordBotApi.Rest;

namespace DiscordBotApi;

public partial class DiscordBotClient
{
	// https://discord.com/developers/docs/interactions/application-commands#create-guild-application-command
	public async Task<DiscordApplicationCommand> CreateGuildApplicationCommandAsync(
		ulong applicationId,
		ulong guildId,
		DiscordCreateGuildApplicationCommandArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"applications/{applicationId}/guilds/{guildId}/commands";

		var argsDto = new DiscordCreateGuildApplicationCommandArgsDto(model: args);
		var applicationCommandDto = await _restClient.SendRequestAsync<DiscordApplicationCommandDto>(
				requestFactoryFunc: () =>
				{
					var request = new HttpRequestMessage(method: HttpMethod.Post, requestUri: url);
					request.Content = _restClient.CreateJsonContent(value: argsDto);
					return request;
				},
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var applicationCommand = new DiscordApplicationCommand(dto: applicationCommandDto);
		return applicationCommand;
	}

	// https://discord.com/developers/docs/interactions/application-commands#delete-guild-application-command
	public async Task DeleteGuildApplicationCommandAsync(
		ulong applicationId,
		ulong guildId,
		ulong commandId,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"applications/{applicationId}/guilds/{guildId}/commands/{commandId}";

		await _restClient.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Delete, requestUri: url),
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	// https://discord.com/developers/docs/interactions/application-commands#get-global-application-commands
	public async Task<IReadOnlyCollection<DiscordApplicationCommand>> GetGlobalApplicationCommandsAsync(
		ulong applicationId,
		DiscordGetApplicationCommandsArgs? args = null,
		CancellationToken cancellationToken = default
	)
	{
		var builder = new QueryBuilder(pathWithoutQuery: $"applications/{applicationId}/commands");
		builder.Add(key: "with_localizations", value: args?.WithLocalizations);

		var applicationCommandDtos = await _restClient.SendRequestAsync<DiscordApplicationCommandDto[]>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: builder.ToString()),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var applicationCommands = applicationCommandDtos.Select(selector: c => new DiscordApplicationCommand(dto: c))
			.ToArray();

		return applicationCommands;
	}

	// https://discord.com/developers/docs/interactions/application-commands#get-guild-application-commands
	public async Task<IReadOnlyCollection<DiscordApplicationCommand>> GetGuildApplicationCommandsAsync(
		ulong applicationId,
		ulong guildId,
		DiscordGetApplicationCommandsArgs? args = null,
		CancellationToken cancellationToken = default
	)
	{
		var builder = new QueryBuilder(pathWithoutQuery: $"applications/{applicationId}/guilds/{guildId}/commands");
		builder.Add(key: "with_localizations", value: args?.WithLocalizations);

		var applicationCommandDtos = await _restClient.SendRequestAsync<DiscordApplicationCommandDto[]>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: builder.ToString()),
				cancellationToken: cancellationToken)
			.ConfigureAwait(continueOnCapturedContext: false);

		var applicationCommands = applicationCommandDtos.Select(selector: c => new DiscordApplicationCommand(dto: c))
			.ToArray();

		return applicationCommands;
	}
}