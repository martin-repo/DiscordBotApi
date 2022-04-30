// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackMessage.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions
{
    using DiscordBotApi.Models.Guilds.Channels.Messages;
    using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
    using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

    public record DiscordInteractionCallbackMessage : DiscordInteractionCallbackData
    {
        public IReadOnlyCollection<DiscordMessageComponent>? Components { get; init; }

        public string? Content { get; init; }

        public IReadOnlyCollection<DiscordEmbed>? Embeds { get; init; }

        public DiscordMessageFlags? Flags { get; init; }
    }
}