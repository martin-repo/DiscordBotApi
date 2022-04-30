// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordRoleDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    using System.Text.Json.Serialization;

    internal record DiscordRoleDto(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("permissions")] string Permissions);
}