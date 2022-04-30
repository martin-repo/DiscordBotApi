// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordGetGuildArgsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds
{
    using System.Text.Json.Serialization;

    internal record DiscordGetGuildArgsDto([property: JsonPropertyName("with_counts")] bool? WithCounts)
    {
        internal DiscordGetGuildArgsDto(DiscordGetGuildArgs model)
            : this(model.WithCounts)
        {
        }
    }
}