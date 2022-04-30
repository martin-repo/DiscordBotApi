// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionData.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions
{
    using DiscordBotApi.Models.Applications;
    using DiscordBotApi.Models.Guilds.Channels.Messages.Components;

    public record DiscordInteractionData
    {
        internal DiscordInteractionData(DiscordInteractionDataDto dto)
        {
            Id = dto.Id != null ? ulong.Parse(dto.Id) : null;
            Name = dto.Name;
            Type = dto.Type != null ? (DiscordApplicationCommandType)dto.Type : null;
            Options = dto.Options?.Select(o => new DiscordApplicationCommandInteractionDataOption(o)).ToArray();
            CustomId = dto.CustomId;
            ComponentType = dto.ComponentType != null ? (DiscordMessageComponentType)dto.ComponentType : null;
        }

        public DiscordMessageComponentType? ComponentType { get; init; }

        public string? CustomId { get; init; }

        public ulong? Id { get; init; }

        public string? Name { get; init; }

        public IReadOnlyCollection<DiscordApplicationCommandInteractionDataOption>? Options { get; init; }

        public DiscordApplicationCommandType? Type { get; init; }
    }
}