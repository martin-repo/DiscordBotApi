// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandPermissionsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/interactions/application-commands#application-command-permissions-object-application-command-permissions-structure
    internal record DiscordApplicationCommandPermissionsDto(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("type")] int Type,
        [property: JsonPropertyName("permission")] bool Permission);
}