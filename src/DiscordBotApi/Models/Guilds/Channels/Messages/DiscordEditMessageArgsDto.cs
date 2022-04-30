// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordEditMessageArgsDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
    using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

    internal record DiscordEditMessageArgsDto(
        [property: JsonPropertyName("content")] string? Content,
        [property: JsonPropertyName("embeds")] DiscordEmbedDto[]? Embeds,
        [property: JsonPropertyName("components")] DiscordMessageComponentDto[]? Components,
        [property: JsonPropertyName("attachments")] DiscordMessageAttachmentDto[]? Attachments)
    {
        internal DiscordEditMessageArgsDto(DiscordEditMessageArgs model)
            : this(
                model.Content,
                model.Embeds?.Select(e => new DiscordEmbedDto(e)).ToArray(),
                model.Components?.Select(DiscordMessageComponent.ConvertToDto).ToArray(),
                model.Attachments?.Select(a => new DiscordMessageAttachmentDto(a)).ToArray())
        {
        }
    }
}