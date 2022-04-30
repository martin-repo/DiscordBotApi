// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateGuildApplicationCommandArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Applications
{
    public record DiscordCreateGuildApplicationCommandArgs
    {
        public bool? DefaultPermission { get; init; }

        public string Description { get; init; } = "";

        public string Name { get; init; } = "";

        public IReadOnlyCollection<DiscordApplicationCommandOption>? Options { get; init; }

        public DiscordApplicationCommandType? Type { get; init; }
    }
}