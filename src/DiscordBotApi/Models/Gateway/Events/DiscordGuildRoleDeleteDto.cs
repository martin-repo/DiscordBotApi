// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildRoleDeleteDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/topics/gateway#guild-role-delete-guild-role-delete-event-fields
    internal record DiscordGuildRoleDeleteDto(
        [property: JsonPropertyName("guild_id")] string GuildId,
        [property: JsonPropertyName("role_id")] string RoleId);
}