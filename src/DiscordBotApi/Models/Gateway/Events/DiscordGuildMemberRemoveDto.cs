// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGuildMemberRemoveDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Events
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Users;

    // https://discord.com/developers/docs/topics/gateway#guild-member-remove-guild-member-remove-event-fields
    internal record DiscordGuildMemberRemoveDto(
        [property: JsonPropertyName("guild_id")] string GuildId,
        [property: JsonPropertyName("user")] DiscordUserDto User);
}