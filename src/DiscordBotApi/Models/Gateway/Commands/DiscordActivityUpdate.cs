// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordActivityUpdate.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Gateway.Commands
{
    public record DiscordActivityUpdate
    {
        public string Name { get; init; } = "";

        public DiscordActivityType Type { get; init; }

        public string? Url { get; init; }
    }
}