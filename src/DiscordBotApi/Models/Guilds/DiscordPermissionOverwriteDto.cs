// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordPermissionsOverwriteDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    using System.Text.Json.Serialization;

    internal record DiscordPermissionOverwriteDto(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("type")] int Type,
        [property: JsonPropertyName("allow")] string Allow,
        [property: JsonPropertyName("deny")] string Deny)
    {
        internal DiscordPermissionOverwriteDto(DiscordPermissionOverwrite model)
            : this(model.Id.ToString(), (int)model.Type, ((ulong)model.Allow).ToString(), ((ulong)model.Deny).ToString())
        {
        }
    }
}