// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordStartThreadWithMessageArgsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels
{
    using System.Text.Json.Serialization;

    internal record DiscordStartThreadWithMessageArgsDto(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("auto_archive_duration")] int? AutoArchiveDuration)
    {
        internal DiscordStartThreadWithMessageArgsDto(DiscordStartThreadWithMessageArgs model)
            : this(model.Name, model.AutoArchiveDuration != null ? (int)model.AutoArchiveDuration : null)
        {
        }
    }
}