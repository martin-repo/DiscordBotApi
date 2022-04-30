// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOptionChoice.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    using DiscordBotApi.Utilities;

    public record DiscordApplicationCommandOptionChoice()
    {
        internal DiscordApplicationCommandOptionChoice(DiscordApplicationCommandOptionType type, DiscordApplicationCommandOptionChoiceDto dto)
            : this()
        {
            Name = dto.Name;
            Value = JsonParseUtils.ToObject(type, dto.Value);
        }

        public string Name { get; init; } = "";

        public object Value { get; init; } = "";
    }
}