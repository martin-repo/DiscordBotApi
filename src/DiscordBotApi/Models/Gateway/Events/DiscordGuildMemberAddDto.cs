// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberAddDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Guilds;
    using DiscordBotApi.Models.Users;

    // https://discord.com/developers/docs/topics/gateway#guild-member-add
    internal record DiscordGuildMemberAddDto(
        DiscordUserDto? User,
        string? Nick,
        string[] Roles,
        [property: JsonPropertyName("guild_id")] string GuildId) : DiscordGuildMemberDto(User, Nick, Roles);
}