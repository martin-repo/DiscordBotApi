// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInstallParamsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/resources/application#install-params-object-install-params-structure
    internal record DiscordInstallParamsDto(
        [property: JsonPropertyName("scopes")] string[] Scopes,
        [property: JsonPropertyName("permissions")] string Permissions);
}