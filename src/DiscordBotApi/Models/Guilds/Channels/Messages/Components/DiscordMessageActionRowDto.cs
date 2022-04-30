// -------------------------------------------------------------------------------------------------
// <copyright file="DiscordMessageActionRowDto.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Models.Guilds.Channels.Messages.Components
{
    using System.Text.Json.Serialization;

    // https://discord.com/developers/docs/interactions/message-components#action-rows
    internal record DiscordMessageActionRowDto(
        [property: JsonPropertyName("components")] DiscordMessageComponentDto[] Components) : DiscordMessageComponentDto(
        (int)DiscordMessageComponentType.ActionRow)
    {
        internal DiscordMessageActionRowDto(DiscordMessageActionRow model)
            : this(model.Components.Select(DiscordMessageComponent.ConvertToDto).ToArray())
        {
        }
    }
}