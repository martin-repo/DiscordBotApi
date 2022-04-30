// -------------------------------------------------------------------------------------------------
// <copyright file="UnavailableGuildDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/resources/guild#unavailable-guild-object
    internal record UnavailableGuildDto([property: JsonPropertyName("id")] string Id, [property: JsonPropertyName("unavailable")] bool? Unavailable);
}