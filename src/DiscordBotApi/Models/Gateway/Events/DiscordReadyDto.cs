// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordReadyDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Applications;
    using DiscordBotApi.Models.Users;

    // https://discord.com/developers/docs/topics/gateway#ready-ready-event-fields
    internal record DiscordReadyDto(
        [property: JsonPropertyName("v")] int V,
        [property: JsonPropertyName("user")] DiscordUserDto User,
        [property: JsonPropertyName("guilds")] UnavailableGuildDto[] Guilds,
        [property: JsonPropertyName("session_id")] string SessionId,
        [property: JsonPropertyName("shard")] int[]? Shard,
        [property: JsonPropertyName("application")] DiscordApplicationDto Application);
}