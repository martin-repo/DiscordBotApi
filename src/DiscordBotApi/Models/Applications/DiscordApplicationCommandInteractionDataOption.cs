// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandInteractionDataOption.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    using DiscordBotApi.Utilities;

    public record DiscordApplicationCommandInteractionDataOption
    {
        internal DiscordApplicationCommandInteractionDataOption(DiscordApplicationCommandInteractionDataOptionDto dto)
        {
            Name = dto.Name;
            Type = (DiscordApplicationCommandOptionType)dto.Type;
            Value = JsonParseUtils.ToObject((DiscordApplicationCommandOptionType)dto.Type, dto.Value);
            Options = dto.Options?.Select(o => new DiscordApplicationCommandInteractionDataOption(o)).ToArray();
            Focused = dto.Focused;
        }

        public bool? Focused { get; init; }

        public string Name { get; init; }

        public IReadOnlyCollection<DiscordApplicationCommandInteractionDataOption>? Options { get; init; }

        public DiscordApplicationCommandOptionType Type { get; init; }

        public object Value { get; init; }
    }
}