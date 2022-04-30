// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionDataDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Applications;

    internal record DiscordInteractionDataDto(
        [property: JsonPropertyName("id")] string? Id,
        [property: JsonPropertyName("name")] string? Name,
        [property: JsonPropertyName("type")] int? Type,
        [property: JsonPropertyName("options")] DiscordApplicationCommandInteractionDataOptionDto[]? Options,
        [property: JsonPropertyName("custom_id")] string? CustomId,
        [property: JsonPropertyName("component_type")] int? ComponentType);
}