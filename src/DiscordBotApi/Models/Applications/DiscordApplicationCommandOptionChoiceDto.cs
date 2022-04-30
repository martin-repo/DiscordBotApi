// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOptionChoiceDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/interactions/application-commands#application-command-object-application-command-option-choice-structure
    internal record DiscordApplicationCommandOptionChoiceDto(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("value")] object Value)
    {
        internal DiscordApplicationCommandOptionChoiceDto(DiscordApplicationCommandOptionChoice model)
            : this(model.Name, model.Value)
        {
        }
    }
}