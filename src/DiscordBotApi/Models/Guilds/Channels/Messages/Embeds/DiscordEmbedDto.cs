// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEmbedDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Embeds
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Utilities;

    // https://discord.com/developers/docs/resources/channel#embed-object-embed-structure
    internal record DiscordEmbedDto(
        [property: JsonPropertyName("title")] string? Title,
        [property: JsonPropertyName("description")] string? Description,
        [property: JsonPropertyName("url")] string? Url,
        [property: JsonPropertyName("color")] int? Color,
        [property: JsonPropertyName("footer")] DiscordFooterDto? Footer,
        [property: JsonPropertyName("image")] DiscordImageDto? Image,
        [property: JsonPropertyName("thumbnail")] DiscordThumbnailDto? Thumbnail,
        [property: JsonPropertyName("video")] DiscordVideoDto? Video,
        [property: JsonPropertyName("author")] DiscordAuthorDto? Author,
        [property: JsonPropertyName("fields")] DiscordFieldDto[]? Fields)
    {
        internal DiscordEmbedDto(DiscordEmbed model)
            : this(
                model.Title,
                model.Description,
                model.Url,
                ColorUtils.ColorToInt(model.Color),
                model.Footer != null ? new DiscordFooterDto(model.Footer) : null,
                model.Image != null ? new DiscordImageDto(model.Image) : null,
                model.Thumbnail != null ? new DiscordThumbnailDto(model.Thumbnail) : null,
                model.Video != null ? new DiscordVideoDto(model.Video) : null,
                model.Author != null ? new DiscordAuthorDto(model.Author) : null,
                model.Fields?.Select(f => new DiscordFieldDto(f)).ToArray())
        {
        }
    }
}