// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildChannelArgsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    using System.Text.Json.Serialization;

    internal record DiscordCreateGuildChannelArgsDto(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("type")] int? Type,
        [property: JsonPropertyName("topic")] string? Topic,
        [property: JsonPropertyName("permission_overwrites")] DiscordPermissionOverwriteDto[]? PermissionOverwrites,
        [property: JsonPropertyName("parent_id")] string? ParentId)
    {
        internal DiscordCreateGuildChannelArgsDto(DiscordCreateGuildChannelArgs model)
            : this(
                model.Name,
                model.Type != null ? (int)model.Type : null,
                model.Topic,
                model.PermissionOverwrites?.Select(po => new DiscordPermissionOverwriteDto(po)).ToArray(),
                model.ParentId != null ? model.ParentId.ToString() : null)
        {
        }
    }
}