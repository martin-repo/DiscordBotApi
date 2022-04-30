// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordCreateMessageArgsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
    using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

    // https://discord.com/developers/docs/resources/channel#create-message-jsonform-paramsc
    internal record DiscordCreateMessageArgsDto(
        [property: JsonPropertyName("content")] string? Content,
        [property: JsonPropertyName("embeds")] DiscordEmbedDto[]? Embeds,
        [property: JsonPropertyName("message_reference")] DiscordMessageReferenceDto? MessageReference,
        [property: JsonPropertyName("components")] DiscordMessageComponentDto[]? Components,
        [property: JsonPropertyName("attachments")] DiscordMessageAttachmentDto[]? Attachments)
    {
        internal DiscordCreateMessageArgsDto(DiscordCreateMessageArgs model)
            : this(
                model.Content,
                model.Embeds?.Select(e => new DiscordEmbedDto(e)).ToArray(),
                model.MessageReference != null ? new DiscordMessageReferenceDto(model.MessageReference) : null,
                model.Components?.Select(DiscordMessageComponent.ConvertToDto).ToArray(),
                model.Attachments?.Select(a => new DiscordMessageAttachmentDto(a)).ToArray())
        {
        }
    }
}