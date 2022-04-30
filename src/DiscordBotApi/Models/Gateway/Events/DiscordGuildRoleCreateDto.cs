// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildRoleCreateDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Guilds;

    // https://discord.com/developers/docs/topics/gateway#guild-role-create-guild-role-create-event-fields
    internal record DiscordGuildRoleCreateDto(
        [property: JsonPropertyName("guild_id")] string GuildId,
        [property: JsonPropertyName("role")] DiscordRoleDto Role);
}