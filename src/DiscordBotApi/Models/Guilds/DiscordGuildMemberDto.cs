// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Users;

    internal record DiscordGuildMemberDto(
        [property: JsonPropertyName("user")] DiscordUserDto? User,
        [property: JsonPropertyName("nick")] string? Nick,
        [property: JsonPropertyName("roles")] string[] Roles);
}