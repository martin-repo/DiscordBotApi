// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildRoleUpdateDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Guilds;

    // https://discord.com/developers/docs/topics/gateway#guild-role-update-guild-role-update-event-fields
    internal record DiscordGuildRoleUpdateDto(
        [property: JsonPropertyName("guild_id")] string GuildId,
        [property: JsonPropertyName("role")] DiscordRoleDto Role);
}