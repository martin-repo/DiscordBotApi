// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordApplicationCommandOption.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    using DiscordBotApi.Models.Guilds.Channels;
    using DiscordBotApi.Utilities;

    public record DiscordApplicationCommandOption()
    {
        internal DiscordApplicationCommandOption(DiscordApplicationCommandOptionDto dto)
            : this()
        {
            var type = (DiscordApplicationCommandOptionType)dto.Type;
            Type = type;
            Name = dto.Name;
            Description = dto.Description;
            Required = dto.Required;
            Choices = dto.Choices?.Select(c => new DiscordApplicationCommandOptionChoice(type, c)).ToArray();
            Options = dto.Options?.Select(o => new DiscordApplicationCommandOption(o)).ToArray();
            ChannelTypes = dto.ChannelTypes?.Select(t => (DiscordChannelType)t).ToArray();
            MinValue = dto.MinValue != null ? JsonParseUtils.ToObject(type, dto.MinValue) : null;
            MaxValue = dto.MaxValue != null ? JsonParseUtils.ToObject(type, dto.MaxValue) : null;
            Autocomplete = dto.Autocomplete;
        }

        public bool? Autocomplete { get; init; }

        public IReadOnlyCollection<DiscordChannelType>? ChannelTypes { get; init; }

        public IReadOnlyCollection<DiscordApplicationCommandOptionChoice>? Choices { get; init; }

        public string Description { get; init; } = "";

        public object? MaxValue { get; init; }

        public object? MinValue { get; init; }

        public string Name { get; init; } = "";

        public IReadOnlyCollection<DiscordApplicationCommandOption>? Options { get; init; }

        public bool? Required { get; init; }

        public DiscordApplicationCommandOptionType Type { get; init; }
    }
}