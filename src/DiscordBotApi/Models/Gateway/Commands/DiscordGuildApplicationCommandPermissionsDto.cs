// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildApplicationCommandPermissionsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-guild-application-command-permissions-structure
    internal record DiscordGuildApplicationCommandPermissionsDto(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("application_id")] string ApplicationId,
        [property: JsonPropertyName("guild_id")] string GuildId,
        [property: JsonPropertyName("permissions")] DiscordApplicationCommandPermissionsDto[] Permissions);
}