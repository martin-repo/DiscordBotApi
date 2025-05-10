// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientApplication.cs" company="kpop.fan">
//   Copyright (c) 2025 kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using System.Collections.Immutable;
using System.Net;

using DiscordBotApi.Interface.Models.Applications;
using DiscordBotApi.Models.Applications;
using DiscordBotApi.Rest;

namespace DiscordBotApi;

public partial class DiscordBotClient
{
	// https://discord.com/developers/docs/interactions/application-commands#create-global-application-command
	public async Task<DiscordApplicationCommand> CreateGlobalApplicationCommandAsync(
		ulong applicationId,
		DiscordCreateGlobalApplicationCommandArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"applications/{applicationId}/commands";

		var argsDto = DiscordCreateGlobalApplicationCommandArgsDto.FromModel(model: args);
		var applicationCommandDto = await _restClient
			.SendRequestAsync<DiscordApplicationCommandDto>(
				requestFactoryFunc: () =>
				{
					var request = new HttpRequestMessage(method: HttpMethod.Post, requestUri: url);
					request.Content = _restClient.CreateJsonContent(value: argsDto);
					return request;
				},
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);

		var applicationCommand = applicationCommandDto.ToModel();
		return applicationCommand;
	}

	// https://discord.com/developers/docs/interactions/application-commands#create-guild-application-command
	public async Task<DiscordApplicationCommand> CreateGuildApplicationCommandAsync(
		ulong applicationId,
		ulong guildId,
		DiscordCreateGuildApplicationCommandArgs args,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"applications/{applicationId}/guilds/{guildId}/commands";

		var argsDto = DiscordCreateGuildApplicationCommandArgsDto.FromModel(model: args);
		var applicationCommandDto = await _restClient
			.SendRequestAsync<DiscordApplicationCommandDto>(
				requestFactoryFunc: () =>
				{
					var request = new HttpRequestMessage(method: HttpMethod.Post, requestUri: url);
					request.Content = _restClient.CreateJsonContent(value: argsDto);
					return request;
				},
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);

		var applicationCommand = applicationCommandDto.ToModel();
		return applicationCommand;
	}

	// https://discord.com/developers/docs/interactions/application-commands#delete-global-application-command
	public async Task DeleteGlobalApplicationCommandAsync(
		ulong applicationId,
		ulong commandId,
		CancellationToken cancellationToken = default
	)
	{
		var url = $"applications/{applicationId}/commands/{commandId}";

		await _restClient
			.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Delete, requestUri: url),
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);
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

		await _restClient
			.SendRequestAsync(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Delete, requestUri: url),
				expectedResponseCode: HttpStatusCode.NoContent,
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);
	}

	// https://discord.com/developers/docs/interactions/application-commands#get-global-application-commands
	public async Task<ImmutableArray<DiscordApplicationCommand>> GetGlobalApplicationCommandsAsync(
		ulong applicationId,
		DiscordGetApplicationCommandsArgs? args = null,
		CancellationToken cancellationToken = default
	)
	{
		var builder = new QueryBuilder(pathWithoutQuery: $"applications/{applicationId}/commands");
		builder.Add(key: "with_localizations", value: args?.WithLocalizations);

		var applicationCommandDtos = await _restClient
			.SendRequestAsync<DiscordApplicationCommandDto[]>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: builder.ToString()),
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);

		var applicationCommands = applicationCommandDtos.Select(selector: c => c.ToModel()).ToImmutableArray();

		return applicationCommands;
	}

	// https://discord.com/developers/docs/interactions/application-commands#get-guild-application-commands
	public async Task<ImmutableArray<DiscordApplicationCommand>> GetGuildApplicationCommandsAsync(
		ulong applicationId,
		ulong guildId,
		DiscordGetApplicationCommandsArgs? args = null,
		CancellationToken cancellationToken = default
	)
	{
		var builder = new QueryBuilder(pathWithoutQuery: $"applications/{applicationId}/guilds/{guildId}/commands");
		builder.Add(key: "with_localizations", value: args?.WithLocalizations);

		var applicationCommandDtos = await _restClient
			.SendRequestAsync<DiscordApplicationCommandDto[]>(
				requestFactoryFunc: () => new HttpRequestMessage(method: HttpMethod.Get, requestUri: builder.ToString()),
				cancellationToken: cancellationToken
			)
			.ConfigureAwait(continueOnCapturedContext: false);

		var applicationCommands = applicationCommandDtos.Select(selector: c => c.ToModel()).ToImmutableArray();

		return applicationCommands;
	}
}