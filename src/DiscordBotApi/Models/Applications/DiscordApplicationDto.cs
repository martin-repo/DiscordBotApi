// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Users;

    // https://discord.com/developers/docs/resources/application#application-object-application-structure
    internal record DiscordApplicationDto(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("bot_public")] bool BotPublic,
        [property: JsonPropertyName("bot_require_code_grant")] bool BotRequireCodeGrant,
        [property: JsonPropertyName("owner")] DiscordUserDto? Owner,
        [property: JsonPropertyName("flags")] int? Flags,
        [property: JsonPropertyName("tags")] string[]? Tags,
        [property: JsonPropertyName("install_params")] DiscordInstallParamsDto? InstallParams,
        [property: JsonPropertyName("custom_install_url")] string[]? CustomInstallUrl);
}