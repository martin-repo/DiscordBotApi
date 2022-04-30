// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOptionDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-structure
    internal record DiscordApplicationCommandOptionDto(
        [property: JsonPropertyName("type")] int Type,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("required")] bool? Required,
        [property: JsonPropertyName("choices")] DiscordApplicationCommandOptionChoiceDto[]? Choices,
        [property: JsonPropertyName("options")] DiscordApplicationCommandOptionDto[]? Options,
        [property: JsonPropertyName("channel_types")] int[]? ChannelTypes,
        [property: JsonPropertyName("min_value")] object? MinValue,
        [property: JsonPropertyName("max_value")] object? MaxValue,
        [property: JsonPropertyName("autocomplete")] bool? Autocomplete)
    {
        internal DiscordApplicationCommandOptionDto(DiscordApplicationCommandOption model)
            : this(
                (int)model.Type,
                model.Name,
                model.Description,
                model.Required,
                model.Choices?.Select(c => new DiscordApplicationCommandOptionChoiceDto(c))
                     .ToArray(),
                model.Options?.Select(o => new DiscordApplicationCommandOptionDto(o)).ToArray(),
                model.ChannelTypes?.Select(t => (int)t).ToArray(),
                model.MinValue,
                model.MaxValue,
                model.Autocomplete)
        {
        }
    }
}