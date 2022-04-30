// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildApplicationCommandArgsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    using System.Text.Json.Serialization;

    internal record DiscordCreateGuildApplicationCommandArgsDto(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("options")] IReadOnlyCollection<DiscordApplicationCommandOptionDto>? Options,
        [property: JsonPropertyName("default_permission")] bool? DefaultPermission,
        [property: JsonPropertyName("type")] int? Type)
    {
        internal DiscordCreateGuildApplicationCommandArgsDto(DiscordCreateGuildApplicationCommandArgs model)
            : this(
                model.Name,
                model.Description,
                model.Options?.Select(o => new DiscordApplicationCommandOptionDto(o)).ToArray(),
                model.DefaultPermission,
                model.Type != null ? (int)model.Type : null)
        {
        }
    }
}