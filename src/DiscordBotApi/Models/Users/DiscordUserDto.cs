// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordUserDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Users
{
    using System.Text.Json.Serialization;

    internal record DiscordUserDto(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("username")] string Username,
        [property: JsonPropertyName("discriminator")] string Discriminator,
        [property: JsonPropertyName("avatar")] string? Avatar,
        [property: JsonPropertyName("bot")] bool? Bot);
}