// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandInteractionDataOptionDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-interaction-data-option-structure
    internal record DiscordApplicationCommandInteractionDataOptionDto(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("type")] int Type,
        [property: JsonPropertyName("value")] object Value,
        [property: JsonPropertyName("options")] DiscordApplicationCommandInteractionDataOptionDto[]? Options,
        [property: JsonPropertyName("focused")] bool? Focused);
}