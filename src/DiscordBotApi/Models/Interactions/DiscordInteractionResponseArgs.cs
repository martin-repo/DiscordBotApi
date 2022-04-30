// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionResponseArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions
{
    public record DiscordInteractionResponseArgs
    {
        public DiscordInteractionCallbackData? Data { get; init; }

        public DiscordInteractionCallbackType Type { get; init; }
    }
}