// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordModifyThreadArgsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    using System.Text.Json.Serialization;

    internal record DiscordModifyThreadArgsDto([property: JsonPropertyName("archived")] bool? Archived)
    {
        internal DiscordModifyThreadArgsDto(DiscordModifyThreadArgs model)
            : this(model.Archived)
        {
        }
    }
}