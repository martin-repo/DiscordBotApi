// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateMessageArgs.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
    using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

    public record DiscordCreateMessageArgs
    {
        public IReadOnlyCollection<DiscordMessageAttachment>? Attachments { get; init; }

        public IReadOnlyCollection<DiscordMessageComponent>? Components { get; init; }

        public string? Content { get; init; }

        public IReadOnlyCollection<DiscordEmbed>? Embeds { get; init; }

        public IReadOnlyCollection<DiscordMessageFile>? Files { get; init; }

        public DiscordMessageReference? MessageReference { get; init; }
    }
}