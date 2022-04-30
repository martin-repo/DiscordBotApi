// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberUpdateDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Users;

    // https://discord.com/developers/docs/topics/gateway#guild-member-update-guild-member-update-event-fields
    internal record DiscordGuildMemberUpdateDto(
        [property: JsonPropertyName("guild_id")] string GuildId,
        [property: JsonPropertyName("roles")] string[] Roles,
        [property: JsonPropertyName("user")] DiscordUserDto User,
        [property: JsonPropertyName("nick")] string? Nick);
}