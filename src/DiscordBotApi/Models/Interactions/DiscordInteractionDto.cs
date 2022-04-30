// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Guilds;
    using DiscordBotApi.Models.Guilds.Channels.Messages;
    using DiscordBotApi.Models.Users;

    // https://discord.com/developers/docs/interactions/receiving-and-responding#interaction-object-interaction-structure
    internal record DiscordInteractionDto(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("application_id")] string ApplicationId,
        [property: JsonPropertyName("type")] int Type,
        [property: JsonPropertyName("data")] DiscordInteractionDataDto? Data,
        [property: JsonPropertyName("guild_id")] string? GuildId,
        [property: JsonPropertyName("channel_id")] string? ChannelId,
        [property: JsonPropertyName("member")] DiscordGuildMemberDto? Member,
        [property: JsonPropertyName("user")] DiscordUserDto? User,
        [property: JsonPropertyName("token")] string Token,
        [property: JsonPropertyName("message")] DiscordMessageDto? Message);
}