// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordFieldDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    using System.Text.Json.Serialization;

    internal record DiscordFieldDto(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("value")] string Value,
        [property: JsonPropertyName("inline")] bool? Inline)
    {
        internal DiscordFieldDto(DiscordField model)
            : this(model.Name, model.Value, model.Inline)
        {
        }
    }
}