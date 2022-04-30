// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageButton.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components
{
    using DiscordBotApi.Models.Guilds.Emojis;

    public record DiscordMessageButton() : DiscordMessageComponent
    {
        internal DiscordMessageButton(DiscordMessageButtonDto dto)
            : this()
        {
            Style = (DiscordMessageButtonStyle)dto.Style;
            Label = dto.Label;
            Emoji = dto.Emoji != null ? new DiscordEmoji(dto.Emoji) : null;
            CustomId = dto.CustomId;
            Url = dto.Url;
            Disabled = dto.Disabled;
        }

        public string? CustomId { get; init; }

        public bool? Disabled { get; init; }

        public DiscordEmoji? Emoji { get; init; }

        public string? Label { get; init; }

        public DiscordMessageButtonStyle Style { get; init; }

        public string? Url { get; init; }
    }
}