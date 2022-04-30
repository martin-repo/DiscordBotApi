// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyGuildChannelArgsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    using System.Text.Json.Serialization;

    internal record DiscordModifyGuildChannelArgsDto(
        [property: JsonPropertyName("name")] string? Name,
        [property: JsonPropertyName("type")] int? Type,
        [property: JsonPropertyName("position")] int? Position,
        [property: JsonPropertyName("topic")] string? Topic,
        [property: JsonPropertyName("permission_overwrites")] DiscordPermissionOverwriteDto[]? PermissionOverwrites,
        [property: JsonPropertyName("parent_id")] string? ParentId)
    {
        internal DiscordModifyGuildChannelArgsDto(DiscordModifyGuildChannelArgs model)
            : this(
                model.Name,
                model.Type != null ? (int)model.Type : null,
                model.Position,
                model.Topic,
                model.PermissionOverwrites?.Select(po => new DiscordPermissionOverwriteDto(po)).ToArray(),
                model.ParentId != null ? model.ParentId.ToString() : null)
        {
        }
    }
}