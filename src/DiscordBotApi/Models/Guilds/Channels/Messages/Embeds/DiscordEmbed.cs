// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEmbed.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    using System.Drawing;

    using DiscordBotApi.Utilities;

    public record DiscordEmbed()
    {
        internal DiscordEmbed(DiscordEmbedDto dto)
            : this()
        {
            Title = dto.Title;
            Description = dto.Description;
            Url = dto.Url;
            Color = ColorUtils.IntToColor(dto.Color);
            Footer = dto.Footer != null ? new DiscordFooter(dto.Footer) : null;
            Image = dto.Image != null ? new DiscordImage(dto.Image) : null;
            Thumbnail = dto.Thumbnail != null ? new DiscordThumbnail(dto.Thumbnail) : null;
            Video = dto.Video != null ? new DiscordVideo(dto.Video) : null;
            Author = dto.Author != null ? new DiscordAuthor(dto.Author) : null;
            Fields = dto.Fields?.Select(f => new DiscordField(f)).ToArray();
        }

        public DiscordAuthor? Author { get; init; }

        public Color? Color { get; init; }

        public string? Description { get; init; }

        public IReadOnlyCollection<DiscordField>? Fields { get; init; }

        public DiscordFooter? Footer { get; init; }

        public DiscordImage? Image { get; init; }

        public DiscordThumbnail? Thumbnail { get; init; }

        public string? Title { get; init; }

        public string? Url { get; init; }

        public DiscordVideo? Video { get; init; }
    }
}