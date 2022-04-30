// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-structure
    internal record DiscordApplicationCommandDto(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("type")] int? Type,
        [property: JsonPropertyName("application_id")] string ApplicationId,
        [property: JsonPropertyName("guild_id")] string? GuildId,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("options")] DiscordApplicationCommandOptionDto[]? Options,
        [property: JsonPropertyName("default_member_permissions")] string DefaultMemberPermissions,
        [property: JsonPropertyName("dm_permission")] bool DmPermission,
        [property: JsonPropertyName("version")] string Version);
}