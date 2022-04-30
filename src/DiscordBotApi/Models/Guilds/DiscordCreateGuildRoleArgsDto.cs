// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildRoleArgsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    using System.Text.Json.Serialization;

    internal record DiscordCreateGuildRoleArgsDto(
        [property: JsonPropertyName("name")] string? Name,
        [property: JsonPropertyName("permissions")] string? Permissions)
    {
        internal DiscordCreateGuildRoleArgsDto(DiscordCreateGuildRoleArgs model)
            : this(model.Name, model.Permissions != null ? ((ulong)model.Permissions).ToString() : null)
        {
        }
    }
}