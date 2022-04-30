// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordActivityUpdateDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    using System.Text.Json.Serialization;

    internal record DiscordActivityUpdateDto(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("type")] int Type,
        [property: JsonPropertyName("url")] string? Url)
    {
        internal DiscordActivityUpdateDto(DiscordActivityUpdate model)
            : this(model.Name, (int)model.Type, model.Url)
        {
        }
    }
}