// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordBotClientApplication.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi
{
    using System.Net;

    using DiscordBotApi.Models.Applications;
    using DiscordBotApi.Rest;

    public partial class DiscordBotClient
    {
        // https://discord.com/developers/docs/interactions/application-commands#create-guild-application-command
        public async Task<DiscordApplicationCommand> CreateGuildApplicationCommandAsync(
            ulong applicationId,
            ulong guildId,
            DiscordCreateGuildApplicationCommandArgs args,
            CancellationToken cancellationToken = default)
        {
            var url = $"applications/{applicationId}/guilds/{guildId}/commands";

            var argsDto = new DiscordCreateGuildApplicationCommandArgsDto(args);
            var applicationCommandDto = await _restClient.SendRequestAsync<DiscordApplicationCommandDto>(
                                                             () =>
                                                             {
                                                                 var request = new HttpRequestMessage(HttpMethod.Post, url);
                                                                 request.Content = _restClient.CreateJsonContent(argsDto);
                                                                 return request;
                                                             },
                                                             cancellationToken)
                                                         .ConfigureAwait(false);

            var applicationCommand = new DiscordApplicationCommand(applicationCommandDto);
            return applicationCommand;
        }

        // https://discord.com/developers/docs/interactions/application-commands#delete-guild-application-command
        public async Task DeleteGuildApplicationCommandAsync(
            ulong applicationId,
            ulong guildId,
            ulong commandId,
            CancellationToken cancellationToken = default)
        {
            var url = $"applications/{applicationId}/guilds/{guildId}/commands/{commandId}";

            await _restClient.SendRequestAsync(() => new HttpRequestMessage(HttpMethod.Delete, url), HttpStatusCode.NoContent, cancellationToken)
                             .ConfigureAwait(false);
        }

        // https://discord.com/developers/docs/interactions/application-commands#get-global-application-commands
        public async Task<IReadOnlyCollection<DiscordApplicationCommand>> GetGlobalApplicationCommandsAsync(
            ulong applicationId,
            DiscordGetApplicationCommandsArgs? args = null,
            CancellationToken cancellationToken = default)
        {
            var builder = new QueryBuilder($"applications/{applicationId}/commands");
            builder.Add("with_localizations", args?.WithLocalizations);

            var applicationCommandDtos = await _restClient.SendRequestAsync<DiscordApplicationCommandDto[]>(
                                                              () => new HttpRequestMessage(HttpMethod.Get, builder.ToString()),
                                                              cancellationToken)
                                                          .ConfigureAwait(false);

            var applicationCommands = applicationCommandDtos.Select(c => new DiscordApplicationCommand(c)).ToArray();

            return applicationCommands;
        }

        // https://discord.com/developers/docs/interactions/application-commands#get-guild-application-commands
        public async Task<IReadOnlyCollection<DiscordApplicationCommand>> GetGuildApplicationCommandsAsync(
            ulong applicationId,
            ulong guildId,
            DiscordGetApplicationCommandsArgs? args = null,
            CancellationToken cancellationToken = default)
        {
            var builder = new QueryBuilder($"applications/{applicationId}/guilds/{guildId}/commands");
            builder.Add("with_localizations", args?.WithLocalizations);

            var applicationCommandDtos = await _restClient.SendRequestAsync<DiscordApplicationCommandDto[]>(
                                                              () => new HttpRequestMessage(HttpMethod.Get, builder.ToString()),
                                                              cancellationToken)
                                                          .ConfigureAwait(false);

            var applicationCommands = applicationCommandDtos.Select(c => new DiscordApplicationCommand(c)).ToArray();

            return applicationCommands;
        }
    }
}