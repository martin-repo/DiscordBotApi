// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordInteractionCallbackMessageDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Interactions
{
    using System.Text.Json.Serialization;

    using DiscordBotApi.Models.Guilds.Channels.Messages.Components;
    using DiscordBotApi.Models.Guilds.Channels.Messages.Embeds;

    internal record DiscordInteractionCallbackMessageDto(
        [property: JsonPropertyName("content")] string? Content,
        [property: JsonPropertyName("embeds")] DiscordEmbedDto[]? Embeds,
        [property: JsonPropertyName("flags")] ulong? Flags,
        [property: JsonPropertyName("components")] DiscordMessageComponentDto[]? Components) : DiscordInteractionCallbackDataDto
    {
        internal DiscordInteractionCallbackMessageDto(DiscordInteractionCallbackMessage model)
            : this(
                model.Content,
                model.Embeds?.Select(e => new DiscordEmbedDto(e)).ToArray(),
                model.Flags != null ? (ulong)model.Flags : null,
                model.Components?.Select(DiscordMessageComponent.ConvertToDto).ToArray()) 
        {
        }
    }
}