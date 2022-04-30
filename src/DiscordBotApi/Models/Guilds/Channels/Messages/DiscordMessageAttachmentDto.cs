// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageAttachmentDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/resources/channel#attachment-object-attachment-structure
    internal record DiscordMessageAttachmentDto(
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("filename")] string? Filename,
        [property: JsonPropertyName("description")] string? Description,
        [property: JsonPropertyName("content_type")] string? ContentType,
        [property: JsonPropertyName("size")] int? Size,
        [property: JsonPropertyName("url")] string? Url,
        [property: JsonPropertyName("proxy_url")] string? ProxyUrl,
        [property: JsonPropertyName("height")] int? Height,
        [property: JsonPropertyName("width")] int? Width,
        [property: JsonPropertyName("ephemeral")] bool? Ephemeral)

    {
        internal DiscordMessageAttachmentDto(DiscordMessageAttachment model)
            : this(
                model.Id.ToString(),
                model.Filename,
                model.Description,
                model.ContentType,
                model.Size,
                model.Url,
                model.ProxyUrl,
                model.Height,
                model.Width,
                model.Ephemeral)
        {
        }
    }
}